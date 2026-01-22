using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShotLimit : GunBase
{
    public float maxShots = 5f;
    public float timeToRecharge = 1f;

    private float _currentShots;
    private bool _recharging;

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
            yield return new WaitForEndOfFrame();
        }
        _currentShots = 0;
        _recharging = false;
    }
}
