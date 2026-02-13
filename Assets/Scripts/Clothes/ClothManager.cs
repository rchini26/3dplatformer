using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;

namespace Clothes
{
    public enum ClothType
    {
        Speed,
    }
    public class ClothManager : Singleton<ClothManager>
    {
        public List<ClothSetup> clothSetups;

        public ClothSetup GetClothSetup(ClothType clothType)
        {
            return clothSetups.Find(i => i.clothType == clothType);
        }
    }

    [System.Serializable]
    public class ClothSetup
    {
        public ClothType clothType;
        public Texture2D texture;
    }
}