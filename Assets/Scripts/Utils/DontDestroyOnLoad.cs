using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;

public class DontDestroyOnLoad : Singleton<DontDestroyOnLoad>
{
    protected override void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(transform.root.gameObject); // Persists the root
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
