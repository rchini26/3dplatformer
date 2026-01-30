using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.StateMachine;
using DG.Tweening;

namespace Boss
{
    public enum BossActions 
    {
        Init,
        Idle,
        Walk,
        Attack,
        Dead
    }
    public class BossBase : MonoBehaviour
    {
        public HealthBase healthBase;
        
        [Header("Animation Setup")]
        public float startAnimationDuration = .5f;
        public Ease startAnimationEase = Ease.OutBack;
        public float speed = 8f;
        public List<Transform> waypoints;

        [Header("Attack Setup")] 
        public int attackAmount = 5;
        public float timeBetweenAttacks = .5f;
        public GameObject projectilePrefab;
        public Transform shootPosition;
        public Transform playerTarget;
        public bool bossActive;
        
        private StateMachine<BossActions> stateMachine;

        void Awake()
        {
            Init();
            healthBase.OnKill += OnBossKill;
            bossActive = false;
            SetBossActiveState(false);
        }
        
        void Init()
        {
            stateMachine = new StateMachine<BossActions>();
            stateMachine.Init();

            stateMachine.RegisterStates(BossActions.Init, new BossStateInit());
            stateMachine.RegisterStates(BossActions.Walk, new BossStateWalk());
            stateMachine.RegisterStates(BossActions.Attack, new BossStateAttack());
            stateMachine.RegisterStates(BossActions.Dead, new BossStateDead());

            stateMachine.SwitchState(BossActions.Init, this);
        }

        void Update()
        {
            if (!bossActive) return;
            stateMachine?.Update();
        }
        public void SetBossActiveState(bool active)
        {
            // enable/disable all renderers (including SkinnedMeshRenderer)
            foreach (var r in GetComponentsInChildren<Renderer>(true))
                r.enabled = active;

            // enable/disable all colliders on the boss root (and optionally children)
            var col = GetComponent<Collider>();
            if (col != null) col.enabled = active;

            // set the flag according to the parameter
            bossActive = active;
        }

        #region Walk
        public void GoToRandomPosition(Action onArrive = null)
        {
            if (waypoints == null || waypoints.Count == 0)
            {
                Debug.LogWarning("Empty Waypoints in BossBase");
                onArrive?.Invoke();
                return;
            }
            StartCoroutine(GoToPositionCoRoutine(waypoints[UnityEngine.Random.Range(0, waypoints.Count)], onArrive));
        }

        IEnumerator GoToPositionCoRoutine(Transform t, Action onArrive = null)
        {
            while (Vector3.Distance(transform.position, t.position) > .1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * speed);
                transform.LookAt(t.transform.position);
                yield return new WaitForEndOfFrame();
            }
            onArrive?.Invoke();
        }
        #endregion
        
        #region Attack

        public void StartAttack(Action endCallBack)
        {
            if (!bossActive) return;
            StartCoroutine(AttackCoRoutine(endCallBack));
        }
        
        IEnumerator AttackCoRoutine(Action endCallBack)
        {
            int attacks = 0;
            while (attacks < attackAmount)
            {
                // Shots aim at player
                if (playerTarget != null && shootPosition != null)
                {
                    shootPosition.LookAt(playerTarget);
                }
                
                // Instantiate Projectile to Attack
                if (projectilePrefab != null && shootPosition != null)
                {
                    Instantiate(projectilePrefab, shootPosition.position, shootPosition.rotation);
                }
                else
                {
                    Debug.LogWarning("Projectile Prefab or Shooting Positions not attached in Inspector!");
                }
                
                attacks++;
                yield return new WaitForSeconds(timeBetweenAttacks);
            }
            endCallBack?.Invoke();
        }
        
        #endregion
        
        #region Death

        void OnBossKill()
        {
            stateMachine.SwitchState(BossActions.Dead, this);
        }
        
        #endregion
        
        #region Animation

        public Tween BeginWithAnimation()
        {
            return transform.DOScale(Vector3.zero, startAnimationDuration).SetEase(startAnimationEase).From();
        }

        #endregion
        
        #region State Machine
        public void SwitchState(BossActions state)
        {
            stateMachine.SwitchState(state, this);
        }
        #endregion
        
        #region Debug

        [NaughtyAttributes.Button]
        private void SwitchInit()
        {
            SwitchState(BossActions.Init);
        }
        [NaughtyAttributes.Button]
        void SwitchWalk()
        {
            SwitchState(BossActions.Walk);
        }
        [NaughtyAttributes.Button]
        void SwitchAttack()
        {
            SwitchState(BossActions.Attack);
        }
        [NaughtyAttributes.Button]
        void SwitchDead()
        {
            SwitchState(BossActions.Dead);
        }
        #endregion
    }
}