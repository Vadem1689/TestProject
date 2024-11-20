using System.Collections.Generic;
using System.Linq;
using System.Text;
using _Game.Scripts.Shop.EventBusEvents;
using _Game.Scripts.Shop.InventorySystem.InventoryBehavior;
using _Game.Scripts.Shop.Items;
using UnityEngine;
using Utility.EventBusSystem.EventBus;
using Utility.EventBusSystem.Subscription;

namespace _Game.Scripts.Shop.InventorySystem
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private InventoryCell _inventoryCellPrefab;
        
        private readonly Subscriptions _subscriptions = new Subscriptions();
        
        private readonly List<InventoryCell> _cells = new List<InventoryCell>();
        public List<InventoryCell> Cells => _cells;

        public void Init(Vector2 inventorySize) 
        {
            for (int i = 0; i < inventorySize.x * inventorySize.y; i++)
            {
                var inventoryCell = Instantiate(_inventoryCellPrefab, transform);
                _cells.Add(inventoryCell);
            }
        }

        public void RedrawInventoryCells(List<ItemData> itemsInInventory)
        {
            _cells.ForEach(x=> x.RemoveItem());

            for (int i = 0; i < itemsInInventory.Count; i++)
            {
                _cells[i].SetItem(itemsInInventory[i]);
            }
            
        }

        private void DebugMenu()
        {
            var slots = _cells.Where(x => x.ItemData != null).ToList();
            var stringBuilder = new StringBuilder();
            
            var itemDatas = new List<ItemData>();

            var inventoryName = GetComponentInParent<Inventory>().gameObject.name;

            foreach (var slot in slots.Where(slot => !itemDatas.Contains(slot.ItemData)))
            {
                stringBuilder.AppendLine($"In {inventoryName} : {slot.ItemData.name} : Count {slots.Count(x => x.ItemData == slot.ItemData)}");
                itemDatas.Add(slot.ItemData);
            }

            Debug.Log(stringBuilder);
        }

        private void OnEnable()
        {
            _subscriptions.Add(EventBus.Subscribe<DebugInventoryСontent>(DebugMenu));
        }

        private void OnDisable()
        {
            _subscriptions.UnsubscribeAll();
        }
    }
}