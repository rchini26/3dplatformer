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
        
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
        
        void OnEnable()
        {
            LoadItemsFromSave();
        }

        void Reset()
        {
            foreach (var i in itemSetups)
            {
                i.soInt.value = 0;
            }
        }

        public void LoadItemsFromSave()
        {
            SetByType(ItemType.Coin, SaveManager.Instance.SaveSetup.coins);
            SetByType(ItemType.LifePack, SaveManager.Instance.SaveSetup.lifePack);
        }
        
        public ItemSetup GetItemByType(ItemType itemType)
        {
            return itemSetups.Find(i => i.itemType == itemType);
        }
        
        public void SetByType(ItemType itemType, int amount)
        {
            GetItemByType(itemType).soInt.value = amount;
        }
        
        public void AddByType(ItemType itemType, int amount = 1)
        {
            if (amount < 0) return;
            itemSetups.Find(i => i.itemType == itemType).soInt.value += amount;
            
            SaveManager.Instance.SaveItems();
        }

        public void RemoveByType(ItemType itemType, int amount = 1)
        {
            itemSetups.Find(i => i.itemType == itemType).soInt.value -= amount;
            
            SaveManager.Instance.SaveItems();
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