using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;
using TMPro;

namespace Items
{
    public enum ItemType
    {
        Coin,
        LifePack
    }
    
    public class ItemManager : Singleton<ItemManager>
    {
        public List<ItemSetup> itemSetups;

        void Start()
        {
            Reset();
        }

        void Reset()
        {
            foreach (var i in itemSetups)
            {
                i.soInt.value = 0;
            }
        }

        public void AddByType(ItemType itemType, int amount = 1)
        {
            if (amount < 0) return;
            itemSetups.Find(i => i.itemType == itemType).soInt.value += amount;
        }

        public void RemoveByType(ItemType itemType, int amount = 1)
        {
            if (amount >= 0) return;
            itemSetups.Find(i => i.itemType == itemType).soInt.value -= amount;
        }

        private void AddCoins()
        {
            AddByType(ItemType.Coin);
        }

        private void AddLifePack()
        {
            AddByType(ItemType.LifePack);
        }
    }

    [System.Serializable]
    public class ItemSetup
    {
        public ItemType itemType;
        public SOInt soInt;
        public Sprite icon;
    }
}