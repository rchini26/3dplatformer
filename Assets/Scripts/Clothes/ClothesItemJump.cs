using UnityEngine;
using Player;

namespace Clothes
{
    public class ClothesItemJump : ClothesItemBase
    {
        public float targetJumpForce = 20f;
        
        protected override void Collect()
        {
            base.Collect();
            PlayerController.Instance.ChangeJumpForce(targetJumpForce, duration);
        }

        public override void OnTriggerEnter(Collider other)
        {
            messageUI.ShowMessage("Super Jump");
            base.OnTriggerEnter(other);
        }
    }
}