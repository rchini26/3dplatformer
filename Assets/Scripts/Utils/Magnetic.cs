using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class Magnetic : MonoBehaviour
{
    public float distance = .2f;
    public float coinSpeed = 3f;
    
    void Update()
    {
        if (PlayerController.Instance == null) return;
        
        if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) > distance)
        {
            coinSpeed++;
            transform.position = Vector3.MoveTowards(transform.position, PlayerController.Instance.transform.position, Time.deltaTime * coinSpeed);
        }
    }
}
