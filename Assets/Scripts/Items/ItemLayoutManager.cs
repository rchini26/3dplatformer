using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemLayoutManager : MonoBehaviour
    {
        public ItemLayout itemLayoutPrefab;
        public Transform itemLayoutParent;
        
        public List<ItemLayout> itemLayouts;

        void Start()
        {
            SpawnItems();
        }
        
        void SpawnItems()
        {
            foreach (var setup in ItemManager.Instance.itemSetups)
            {
                var itemLayout = Instantiate(itemLayoutPrefab, itemLayoutParent);
                itemLayout.LoadSetup(setup);
                itemLayouts.Add(itemLayout);
            }
        }
        
    }
}