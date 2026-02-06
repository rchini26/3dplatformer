using System.Collections;
using System;
using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;
using Player;

public class HealthBase : MonoBehaviour, IDamageable
{
    public event Action OnKill;
    public List<UIFillerUpdater> uiHealthUpdater;

    public int startLife = 10;
    public bool destroyOnKill;
    public float delayToKill = .5f;
    public FlashColor flashColor;
    public ParticleSystem particleSystemHit;
    public ParticleSystem particleSystemDeath;

    [SerializeField] private int _currentLife;
    public bool isDead;
    
    void Awake()
    {
        Init();
    }

    void Init()
    {
        ResetLife();
    }

    public virtual void ResetLife()
    {
        isDead = false;
        _currentLife = startLife;
    }

    public void Damage(int amount)
    {
        if (isDead) return;

        _currentLife -= amount;
        UpdateUIHealth();
        if (flashColor != null) flashColor.Flash();
        EffectsManager.Instance.ChangeVignette();
        if (particleSystemHit != null) particleSystemHit.Emit(30);

        if (_currentLife <= 0)
        {
            Kill();
            if (particleSystemDeath != null) particleSystemDeath.Emit(30);
        }
    }

    private void Kill()
    {
        if (isDead) return;
        isDead = true;
        DOTween.Kill(gameObject);

        OnKill?.Invoke();

        if (destroyOnKill)
        {
            Destroy(gameObject, delayToKill);
        }
    }
    
    private void UpdateUIHealth()
    {
        if (uiHealthUpdater != null)
        {
            uiHealthUpdater.ForEach(i => i.UpdateValue((float)_currentLife / startLife));
        }
    }
}
