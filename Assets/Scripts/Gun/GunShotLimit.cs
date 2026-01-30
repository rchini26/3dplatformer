using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GunShotLimit : GunBase
{
    public List<UIFillerUpdater> uiGunUpdater;
    
    public int maxShots = 5;
    public float timeToRecharge = 1f;

    private int _currentShots;
    private bool _recharging;

    private void Awake()
    {
        GetGunUIUpdater();
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
            uiGunUpdater.ForEach(x => x.UpdateValue(time/timeToRecharge));
            yield return new WaitForEndOfFrame();
        }
        _currentShots = 0;
        _recharging = false;
    }

    void UpdateUI()
    {
        uiGunUpdater.ForEach(x => x.UpdateValue(maxShots, _currentShots));
    }

    void GetGunUIUpdater()
    {
        var gunUI = GameObject.Find("GunUpdater");
        if (gunUI != null)
        {
            uiGunUpdater = gunUI.GetComponents<UIFillerUpdater>().ToList();
        }
        else
        {
            uiGunUpdater = new List<UIFillerUpdater>();
        }
    }
}
