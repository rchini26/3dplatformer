using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DestructibleItemBase : MonoBehaviour
{
    public HealthBase healthBase;
    
    [Header("Shake Settings")]
    public float shakeDuration = .1f;
    public int shakeVibrato = 5;

    [Header("Drop Coins")]
    public int dropCoinsAmount = 3;
    public GameObject dropCoinPrefab;
    public Transform dropSpot;

    void OnValidate()
    {
        if (healthBase == null) healthBase = GetComponent<HealthBase>();
    }

    private void Awake()
    {
        OnValidate();
        healthBase.OnDamage += OnDamage;
        healthBase.OnKill += OnKill;
    }

    void OnDamage(HealthBase h)
    {
        transform.DOShakeScale(shakeDuration, Vector3.down, shakeVibrato);
        DropCoins();
    }

    void OnKill()
    {
        DropGroupOfCoins();
    }
    
    void DropCoins()
    {
        var i = Instantiate(dropCoinPrefab, dropSpot.position, Quaternion.identity);
        i.transform.DOScale(Vector3.zero, .5f).SetEase(Ease.OutBack).From();
    }
    
    void DropGroupOfCoins()
    {
        StartCoroutine(DropGroupOfCoinsCoRoutine());
    }
    
    IEnumerator DropGroupOfCoinsCoRoutine()
    {
        for (int i = 0; i < dropCoinsAmount; i++)
        {
            DropCoins();
            yield return new WaitForSeconds(.1f);
        }
    }
}
