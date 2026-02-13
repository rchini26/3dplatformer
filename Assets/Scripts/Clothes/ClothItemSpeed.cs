using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Clothes
{
    public class ClothItemSpeed : ClothItemBase
    {
        public float targetSpeed = 2f;
        public override void Collect()
        {
            base.Collect();
            PlayerController.Instance.ChangeSpeed(targetSpeed, duration);
        }
    }
}