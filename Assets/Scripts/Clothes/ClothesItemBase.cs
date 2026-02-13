using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Clothes
{
    public class ClothesItemBase : MonoBehaviour
    {
        public ClothesType clothType;
        public string compareTag = "Player";
        public float duration = 2f;

        void OnTriggerEnter(Collider collision)
        {
            if (collision.transform.CompareTag(compareTag))
            {
                Collect();
            }
        }

        public virtual void Collect()
        {
            var setup = ClothesManager.Instance.GetClothesSetup(clothType);
            PlayerController.Instance.ChangeTexture(setup, duration);
            HideObject();
        }

        private void HideObject()
        {
            gameObject.SetActive(false);
        }
    }
}