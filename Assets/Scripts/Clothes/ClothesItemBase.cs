using Player;
using UnityEngine;

namespace Clothes
{
    public class ClothesItemBase : MonoBehaviour
    {
        public ClothesType clothType;
        public string compareTag = "Player";
        public float duration = 2f;
        public ClothesUiMessage messageUI;

        public virtual void OnTriggerEnter(Collider collision)
        {
            if (collision.transform.CompareTag(compareTag))
            {
                Collect();
            }
        }

        protected virtual void Collect()
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