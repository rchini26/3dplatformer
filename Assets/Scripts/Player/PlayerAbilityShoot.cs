using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    public GunBase[] guns;
    public Transform gunPosition;

    private GunBase _currentGun;
    private List<GunBase> _instantiatedGuns = new List<GunBase>();
    private int _currentGunIndex = 0;

    protected override void Init()
    {
        base.Init();
        CreateGuns();

        inputs.Gameplay.Shoot.performed += ctx => StartShoot();
        inputs.Gameplay.Shoot.canceled += ctx => CancelShoot();
        inputs.Gameplay.ChangeGuns.performed += ctx => OnChangeGun(ctx);
    }

    void CreateGuns()
    {
        foreach (var gun in guns)
        {
            var instantiatedGun = Instantiate(gun, gunPosition);
            instantiatedGun.transform.localPosition = instantiatedGun.transform.localEulerAngles = Vector3.zero;
            instantiatedGun.gameObject.SetActive(false);
            _instantiatedGuns.Add(instantiatedGun);
        }

        if (_instantiatedGuns.Count > 0)
        {
            _currentGun = _instantiatedGuns[0];
            _currentGun.gameObject.SetActive(true);
        }
    }

    void OnChangeGun(InputAction.CallbackContext ctx)
    {
        string keyPressed = ctx.control.name;

        if (keyPressed == "1" && _instantiatedGuns.Count > 0)
        {
            SwitchToGun(0);
        }
        else if (keyPressed == "2" && _instantiatedGuns.Count > 1)
        {
            SwitchToGun(1);
        }
    }

    void SwitchToGun(int gunIndex)
    {
        if (_currentGun != null)
        {
            _currentGun.CancelShoot();
            _currentGun.gameObject.SetActive(false);
        }

        _currentGunIndex = gunIndex;
        _currentGun = _instantiatedGuns[_currentGunIndex];
        _currentGun.gameObject.SetActive(true);
    }
    
    private void StartShoot()
    {
        _currentGun.StartShoot();
        Debug.Log("Start Shoot"); 
    }
    
    private void CancelShoot()
    {
        _currentGun.CancelShoot();
        Debug.Log("Cancel Shoot"); 
    }
}
