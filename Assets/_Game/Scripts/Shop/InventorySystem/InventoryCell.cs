using _Game.Scripts.Shop.InventorySystem.InventoryItemContainerBehavior;
using _Game.Scripts.Shop.Items;
using NaughtyAttributes;
using UnityEngine;

namespace _Game.Scripts.Shop.InventorySystem
{
    public class InventoryCell : MonoBehaviour
    {
        [SerializeField] private InventoryItemContainer _inventoryItemContainer;
        
        [ShowNativeProperty] public ItemData ItemData => _inventoryItemContainer.Item;

        public void SetItem(ItemData itemData)
        {
            Debug.Log(itemData.name);
            _inventoryItemContainer.AddItem(itemData);
        }

        public void RemoveItem()
        {
            _inventoryItemContainer.Clear();
        }
    }
}