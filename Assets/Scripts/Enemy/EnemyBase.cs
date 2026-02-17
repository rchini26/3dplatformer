using System.Collections;
using UnityEngine;
using DG.Tweening;
using Animation;
using UnityEngine.Events;

namespace Enemy
{
  public class EnemyBase : MonoBehaviour, IDamageable
  {
    [Header("Enemy Health")]
    public int damage = 10;
    public HealthBase healthBase;
    public Collider newCollider;
    
    [Header("Animation Setup")]
    public float timeToDestroy = 1f;
    public float startAnimationDuration = .2f;
    public Ease startAnimationEase = Ease.OutBack;
    public bool startWithAnimation = true;
    public float attackDuration = 1f;
    [SerializeField] private AnimationBase animationBase;
    
    [Header("Events")]
    public UnityEvent OnKillEvent;

    private void Awake()
    {
      Init();
      
      if (healthBase != null)
      {
        healthBase.OnKill += OnEnemyKill;
      }
    }

    protected virtual void Init()
    {
      if(startWithAnimation) BeginWithAnimation();
    }
    
    protected virtual void OnEnemyKill()
    {
      healthBase.OnKill -= OnEnemyKill;
      if(newCollider != null) newCollider.enabled = false;
      PlayAnimationByTrigger(AnimationType.Death);
      Destroy(gameObject, timeToDestroy);
      OnKillEvent?.Invoke();
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
      var health = collision.gameObject.GetComponent<HealthBase>();

      if (health != null)
      {
        health.Damage(damage);
        StartCoroutine(AttackAnimation());
      }
    }

    IEnumerator AttackAnimation()
    {
      PlayAnimationByTrigger(AnimationType.Attack);
      yield return new WaitForSeconds(attackDuration);
      PlayAnimationByTrigger(AnimationType.Idle);
    }

    public void Damage(int amount)
    {
      healthBase.Damage(amount);
    }
    
    #region Animation Methods

    void BeginWithAnimation()
    {
      transform.DOScale(Vector3.zero, startAnimationDuration).SetEase(startAnimationEase).From();
    }

    public void PlayAnimationByTrigger(AnimationType animationType)
    {
      animationBase.PlayAnimationByTrigger(animationType);
    }
    
    #endregion
  }
}
