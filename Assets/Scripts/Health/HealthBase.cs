using System;
using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;
using Player;
using System.Linq;

public class HealthBase : MonoBehaviour, IDamageable
{
    public event Action OnKill;
    public event Action<HealthBase> OnDamage;
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

    void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        FindUIUpdaters();
        UpdateUIHealth();
    }

    void Init()
    {
        FindUIUpdaters();
        ResetLife();
    }

    void FindUIUpdaters()
    {
        UIFillerUpdater[] updaters = FindObjectsOfType<UIFillerUpdater>();
        uiHealthUpdater = updaters
            .Where(u => u.updaterType == UIUpdaterType.Health)
            .ToList();
    }

    public virtual void ResetLife()
    {
        isDead = false;
        _currentLife = startLife;
        UpdateUIHealth();
    }

    public void Damage(int amount)
    {
        if (isDead) return;

        _currentLife -= amount;
        OnDamage?.Invoke(this);
        UpdateUIHealth();
        if (flashColor != null) flashColor.Flash();
        if (particleSystemHit != null) particleSystemHit.Emit(30);
        if (GetComponent<PlayerController>() != null)
        {
            EffectsManager.Instance.ChangeVignette();
            ShakeCamera.Instance.Shake(3f, 3f, 0.5f);
        }

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
        if (uiHealthUpdater != null && CompareTag("Player"))
        {
            uiHealthUpdater.ForEach(i => i.UpdateValue((float)_currentLife / startLife));
        }
    }
}
