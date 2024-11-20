using _Game.Scripts.Shop.InventorySystem.InventoryItemContainerBehavior;
using _Game.Scripts.Shop.Items;
using TMPro;
using UnityEngine;

namespace _Game.Scripts.Shop.InventorySystem
{
    public class InventoryCell : MonoBehaviour
    {
        [SerializeField] private InventoryItemContainer _inventoryItemContainer;
        [SerializeField] private TMP_Text _priceText;
        public ItemData ItemData => _inventoryItemContainer.Item;

        public void SetItem(ItemData itemData)
        {
            _inventoryItemContainer.AddItem(itemData);

            RebuildPriceText(itemData.ItemPrice, itemData.PercentageOfPriceDroppedForPlayer);
        }

        public void RemoveItem()
        {
            _inventoryItemContainer.Clear();
            _priceText.transform.parent.gameObject.SetActive(false);
        }

        private void RebuildPriceText(float price, float PercentageOfPriceDroppedForPlayer)
        {
            _priceText.transform.parent.gameObject.SetActive(true);
            
            _priceText.text = _inventoryItemContainer is PlayerInventoryItemContainer
                ? Mathf.RoundToInt(price * PercentageOfPriceDroppedForPlayer / 100).ToString()
                : Mathf.RoundToInt(price).ToString();
            
        }
    }
}