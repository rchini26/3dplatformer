using System;
using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;

public class PlayerMagneticTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        CollectableItemBase i = other.transform.GetComponent<CollectableItemBase>();
        if (i != null)
        {
            i.gameObject.AddComponent<Magnetic>();
        } 
    }
}
