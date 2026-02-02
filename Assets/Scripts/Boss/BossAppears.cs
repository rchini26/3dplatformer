using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss
{
    public class BossAppears : MonoBehaviour
    {
        public GameObject bossPrefab;
        public BossBase boss;

        void OnTriggerEnter(Collider other)
        {
            if (boss != null && other.gameObject.tag == "Player")
            {
                boss.SetBossActiveState(true);
            }
        }
    }
}