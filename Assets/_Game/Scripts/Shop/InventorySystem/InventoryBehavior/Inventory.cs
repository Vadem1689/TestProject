using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Shop.Interfaces;
using _Game.Scripts.Shop.Items;
using UnityEngine;
using Utility.EventBusSystem.Subscription;

namespace _Game.Scripts.Shop.InventorySystem.InventoryBehavior
{
    public abstract class Inventory : MonoBehaviour, IInventory
    {
        [SerializeField] protected InventoryView _inventoryView;

        public List<ItemData> _itemsInInventory = new List<ItemData>();

        private readonly Vector2 _inventorySize = new Vector2(5, 8);
        
        protected readonly Subscriptions _subscriptions = new Subscriptions();

        public virtual void Start()
        {
            _inventoryView.Init(_inventorySize);
        }

        private bool CheckInventoryToSpace()
        {
            return _inventoryView.Cells.All(cell => cell.ItemData != null);
        }

        private InventoryCell GetFirstFreeCell()
        {
            if (CheckInventoryToSpace())
                throw new Exception("No free cell available");
            
            return _inventoryView.Cells.First(x => x.ItemData == null);
        }

        public void AddItem(ItemData item)
        {
            var cell = GetFirstFreeCell();
            if (cell != null && item != null)
            {
                _itemsInInventory.Add(item);
                cell.SetItem(item);
                _inventoryView.RedrawInventoryCells(_itemsInInventory);
            }
        }

        public void RemoveItem(ItemData item)
        {
            var requiredItemToRemove = _inventoryView.Cells.FirstOrDefault(x => x.ItemData == item);

            if (requiredItemToRemove == null || item == null)
            {
                throw new Exception("Item not found");
            }

            _itemsInInventory.Remove(requiredItemToRemove.ItemData);
            requiredItemToRemove.RemoveItem();
            
            _inventoryView.RedrawInventoryCells(_itemsInInventory);
        }

    }
}