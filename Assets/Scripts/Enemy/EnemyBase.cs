using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Animation;

namespace Enemy
{
  public class EnemyBase : MonoBehaviour, IDamageable
  {
    [Header("Enemy Health")]
    public int damage = 10;
    public HealthBase healthBase;
    public Collider collider;
    
    [Header("Animation Setup")]
    public float timeToDestroy = 1f;
    public float startAnimationDuration = .2f;
    public Ease startAnimationEase = Ease.OutBack;
    public bool startWithAnimation = true;
    [SerializeField] private AnimationBase _animationBase;

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
      if(collider != null) collider.enabled = false;
      PlayAnimationByTrigger(AnimationType.Death);
      Destroy(gameObject, timeToDestroy);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
      var health = collision.gameObject.GetComponent<HealthBase>();

      if (health != null)
      {
        health.Damage(damage);
        PlayAnimationByTrigger(AnimationType.Attack);
      }
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
      _animationBase.PlayAnimationByTrigger(animationType);
    }
    
    #endregion
  }
}
