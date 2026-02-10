using System;
using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;
using Player;

public class LifePackAction : MonoBehaviour
{
    public KeyCode keyCode = KeyCode.R;
    public SOInt soInt;
    private HealthBase _healthBase;

    void Start()
    {
        soInt = ItemManager.Instance.GetItemByType(ItemType.LifePack).soInt;
        _healthBase = FindObjectOfType<PlayerController>()?.GetComponent<HealthBase>();
    }

    private void RecoverLife()
    {
        if (soInt != null && soInt.value > 0)
        {
            ItemManager.Instance.RemoveByType(ItemType.LifePack);

            if (_healthBase != null)
            {
                _healthBase.ResetLife();
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyCode)) RecoverLife();
    }
}
