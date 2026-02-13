using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;

namespace Clothes
{
    public enum ClothesType
    {
        Speed,
    }
    public class ClothesManager : Singleton<ClothesManager>
    {
        public List<ClothesSetup> clothesSetups;

        public ClothesSetup GetClothesSetup(ClothesType clothType)
        {
            return clothesSetups.Find(i => i.clothesType == clothType);
        }
    }

    [System.Serializable]
    public class ClothesSetup
    {
        public ClothesType clothesType;
        public Texture2D texture;
    }
}