using System.Collections;
using System;
using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;

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
    private bool _isDead;

    void Awake()
    {
        Init();
    }

    void Init()
    {
        ResetLife();
    }

    protected virtual void ResetLife()
    {
        _isDead = false;
        _currentLife = startLife;
    }

    public void Damage(int amount)
    {
        if (_isDead) return;

        _currentLife -= amount;
        UpdateUIHealth();
        if (flashColor != null) flashColor.Flash();
        if (particleSystemHit != null) particleSystemHit.Emit(30);

        if (_currentLife <= 0)
        {
            Kill();
            if (particleSystemDeath != null) particleSystemDeath.Emit(30);
        }
    }

    private void Kill()
    {
        if (_isDead) return;
        _isDead = true;
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
