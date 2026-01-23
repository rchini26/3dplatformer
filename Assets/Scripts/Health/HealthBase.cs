using System.Collections;
using System;
using DG.Tweening;
using UnityEngine;

public class HealthBase : MonoBehaviour, IDamageable
{
    public event Action OnKill;
    
    public int startLife = 10;
    public bool destroyOnKill;
    public float delayToKill = .5f;
    
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
        if(_isDead) return;
        
        _currentLife -= amount;
        
        if (_currentLife <= 0)
        {
            Kill();    
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
}
