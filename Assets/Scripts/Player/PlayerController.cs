using UnityEngine;
using Core.StateMachine;
using Core.Singleton;
using System.Collections;
using Clothes;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerController : Singleton<PlayerController>
    {
        public enum PlayerStates
        {
            Idle,
            Walking,
            Jumping,
            Dead
        }

        [Header("Components")] 
        public Rigidbody rb;
        public Animator animator;

        [Header("Movement Settings")] 
        public float moveSpeed = 5f;
        public float rotationSpeed = 180f; // Degrees per second
        public float jumpForce = 10f;

        [Header("Ground Check")] 
        public Transform groundCheck;
        public float groundCheckRadius = 0.2f;
        public LayerMask groundLayer;
        public bool isOnGround { get; private set; }
        
        [Header("Gravity for Better Jumping")] 
        public float fallMultiplier = 5f; // Faster fall

        [Header("Run Setup")] 
        public KeyCode runKey = KeyCode.LeftShift;
        public float runSpeed = 1.5f;
        
        [Space]
        [SerializeField] private ClothesChanger _clothesChanger;
        public StateMachine<PlayerStates> stateMachine;

        // Cached input values
        public Vector2 moveInput { get; private set; }
        public bool hasMovementInput => Mathf.Abs(moveInput.y) > 0.01f; // Only forward/backward counts as movement
        public bool hasRotationInput => Mathf.Abs(moveInput.x) > 0.01f; // Left/right for rotation

        private HealthBase _healthBase;

        protected override void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            if (rb == null) rb = GetComponent<Rigidbody>();
            if (animator == null) animator = GetComponent<Animator>();

            // Only freeze X and Z rotation, allow Y rotation for turning
            rb.freezeRotation = true;
        }

        private void Start()
        {
            _healthBase = GetComponent<HealthBase>();
            if (_healthBase != null)
            {
                _healthBase.OnKill += OnPlayerDeath;
            }

            stateMachine = new StateMachine<PlayerStates>();
            stateMachine.Init();
            stateMachine.RegisterStates(PlayerStates.Idle, new PlayerIdleState(this));
            stateMachine.RegisterStates(PlayerStates.Walking, new PlayerWalkingState(this));
            stateMachine.RegisterStates(PlayerStates.Jumping, new PlayerJumpingState(this));
            stateMachine.RegisterStates(PlayerStates.Dead, new PlayerDeadState(this));
            stateMachine.SwitchState(PlayerStates.Idle);
        }

        private void Update()
        {
            UpdateGroundStatus();
            UpdateInput();
            if (stateMachine.CurrentState is PlayerDeadState) return;
            stateMachine.Update();
            ApplyBetterJumping();

            // Check for Animation
            if (hasMovementInput)
            {
                animator.SetBool("Run", true);
            }
            else
            {
                animator.SetBool("Run", false);
            }
        }

        #region Movement Methods

        private void UpdateInput()
        {
            moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }

        void OnPlayerDeath()
        {
            stateMachine.SwitchState(PlayerStates.Dead);
        }

        private void UpdateGroundStatus()
        {
            bool wasOnGround = isOnGround;
            isOnGround = groundCheck != null && groundLayer != 0
                ? Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer)
                : Physics.Raycast(transform.position, Vector3.down, 1.1f);

            // Auto-transition when landing
            if (!wasOnGround && isOnGround && stateMachine.CurrentState is PlayerJumpingState)
            {
                animator.SetTrigger("Land"); 
                stateMachine.SwitchState(hasMovementInput ? PlayerStates.Walking : PlayerStates.Idle);
            }
        }

        public void ApplyMovement()
        {
            // Handle rotation (left/right input)
            if (hasRotationInput)
            {
                float rotationAmount = moveInput.x * rotationSpeed * Time.deltaTime;
                transform.Rotate(0, rotationAmount, 0);
            }

            // Handle forward/backward movement
            if (hasMovementInput)
            {
                float _currentSpeed = moveSpeed;
                if (Input.GetKey(runKey))
                {
                    _currentSpeed *= runSpeed;
                }

                // Move in the direction the player is facing
                Vector3 moveDirection = transform.forward * moveInput.y;
                Vector3 velocity = rb.velocity;
                velocity.x = moveDirection.x * _currentSpeed;
                velocity.z = moveDirection.z * _currentSpeed;
                rb.velocity = velocity;
            }
        }

        public void StopHorizontalMovement()
        {
            Vector3 velocity = rb.velocity;
            velocity.x = 0;
            velocity.z = 0;
            rb.velocity = velocity;
        }

        public void Jump()
        {
            animator.SetTrigger("Jump");
            Vector3 velocity = rb.velocity;
            velocity.y = jumpForce;
            rb.velocity = velocity;
        }

        void ApplyBetterJumping()
        {
            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
        }

        public void ChangeSpeed(float speed, float duration)
        {
            StartCoroutine(ChangeSpeedCoroutine(speed, duration));
        }

        IEnumerator ChangeSpeedCoroutine(float localSpeed, float duration)
        {
            var defaultSpeed = moveSpeed;
            moveSpeed = localSpeed;
            yield return new WaitForSeconds(duration);
            moveSpeed = defaultSpeed;
        }
        
        public void ChangeJumpForce(float jumpForce, float duration)
        {
            StartCoroutine(ChangeJumpForceCoroutine(jumpForce, duration));
        }

        IEnumerator ChangeJumpForceCoroutine(float localJumpForce, float duration)
        {
            var defaultJumpForce = jumpForce;
            jumpForce = localJumpForce;
            yield return new WaitForSeconds(duration);
            jumpForce = defaultJumpForce;
        }
        
        public void ChangeTexture(ClothesSetup clothSetup, float duration)
        {
            StartCoroutine(ChangeTextureCoroutine(clothSetup, duration));
        }

        IEnumerator ChangeTextureCoroutine(ClothesSetup clothSetup, float duration)
        {
            _clothesChanger.ChangeTexture(clothSetup);
            yield return new WaitForSeconds(duration);
            _clothesChanger.ResetTexture();
        }

        #endregion
    }

    #region Base Class Grounded State

// Base class for grounded states (Idle & Walking)
    public abstract class PlayerGroundedState : StateBase
    {
        protected PlayerController player;

        public PlayerGroundedState(PlayerController player)
        {
            this.player = player;
        }

        public override void OnStateStay()
        {
            // Jump check (shared by Idle and Walking)
            if (Input.GetKeyDown(KeyCode.Space) && player.isOnGround)
            {
                player.stateMachine.SwitchState(PlayerController.PlayerStates.Jumping);
                return;
            }

            HandleMovement();
        }

        protected abstract void HandleMovement();
    }

    #endregion

    #region State Classes

// Idle State
    public class PlayerIdleState : PlayerGroundedState
    {
        public PlayerIdleState(PlayerController player) : base(player)
        {
        }

        protected override void HandleMovement()
        {
            // Check for forward/backward movement OR rotation input
            if (player.hasMovementInput || player.hasRotationInput)
            {
                player.stateMachine.SwitchState(PlayerController.PlayerStates.Walking);
            }
            else
            {
                player.StopHorizontalMovement();
            }
        }
    }

// Walking State
    public class PlayerWalkingState : PlayerGroundedState
    {
        public PlayerWalkingState(PlayerController player) : base(player)
        {
        }

        protected override void HandleMovement()
        {
            // Stay in walking state if there's any movement or rotation input
            if (player.hasMovementInput || player.hasRotationInput)
            {
                player.ApplyMovement();
            }
            else
            {
                player.stateMachine.SwitchState(PlayerController.PlayerStates.Idle);
            }
        }
    }

// Jumping State
    public class PlayerJumpingState : StateBase
    {
        private PlayerController player;

        public PlayerJumpingState(PlayerController player)
        {
            this.player = player;
        }

        public override void OnStateEnter(object o = null)
        {
            player.Jump();
        }

        public override void OnStateStay()
        {
            // Air control - allow rotation and movement while jumping
            player.ApplyMovement();
        }
    }

// Dead State
    public class PlayerDeadState : StateBase
    {
        private PlayerController player;

        public PlayerDeadState(PlayerController player)
        {
            this.player = player;
        }

        public override void OnStateEnter(object o = null)
        {
            player.StopHorizontalMovement();
            player.animator.SetTrigger("Death");
        }
    }

    #endregion
}