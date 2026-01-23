using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GunShotLimit : GunBase
{
    public List<UIGunUpdate> uiGunUpdates;
    
    public int maxShots = 5;
    public float timeToRecharge = 1f;

    private int _currentShots;
    private bool _recharging;

    private void Awake()
    {
        GetAllUIs();
    }

    protected override IEnumerator ShootCoroutine()
    {
        if (_recharging) yield break;

        while (true)
        {
            if (_currentShots < maxShots)
            {
                Shoot();
                _currentShots++;
                CheckRecharge();
                UpdateUI();
                yield return new WaitForSeconds(timeBetweenShots);
            }
            else
            {
                yield break;
            }
        }
    }

    void CheckRecharge()
    {
        if (_currentShots >= maxShots)
        {
            CancelShoot();
            StartRecharge();
        }
    }

    void StartRecharge()
    {
        _recharging = true;
        StartCoroutine(RechargeCoroutine());
    }

    IEnumerator RechargeCoroutine()
    {
        float time = 0;
        while (time < timeToRecharge)
        {
            time += Time.deltaTime;
            uiGunUpdates.ForEach(x => x.UpdateValue(time/timeToRecharge));
            yield return new WaitForEndOfFrame();
        }
        _currentShots = 0;
        _recharging = false;
    }

    void UpdateUI()
    {
        uiGunUpdates.ForEach(x => x.UpdateValue(maxShots, _currentShots));
    }

    void GetAllUIs()
    {
        uiGunUpdates = GameObject.FindObjectsOfType<UIGunUpdate>().ToList();
    }
}
