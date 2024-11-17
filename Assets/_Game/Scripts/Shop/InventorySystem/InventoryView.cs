using System.Collections.Generic;
using _Game.Scripts.Shop.Items;
using UnityEngine;

namespace _Game.Scripts.Shop.InventorySystem
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private InventoryCell _inventoryCellPrefab;
        
        private readonly List<InventoryCell> _cells = new();

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
    }
}