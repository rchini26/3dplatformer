using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;

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

        protected override void Awake()
        {
            base.Awake();
            
            Reset();
        }

        void OnEnable()
        {
            SaveManager.Instance.FileLoaded += OnFileLoaded;
        }

        void OnDisable()
        {
            if (SaveManager.Instance != null)
            {
                SaveManager.Instance.FileLoaded -= OnFileLoaded;
            }
        }

        private void OnFileLoaded(SaveSetup saveSetup)
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
            if (SaveManager.Instance == null || SaveManager.Instance.SaveSetup == null) return;

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