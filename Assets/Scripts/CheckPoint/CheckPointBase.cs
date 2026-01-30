using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointBase : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            VerifyCheckPoint();
        }
    }

    void VerifyCheckPoint()
    {
        
    }

    void TurnItOn()
    {
        
    }

    void TurnItOff()
    {
        
    }
}
