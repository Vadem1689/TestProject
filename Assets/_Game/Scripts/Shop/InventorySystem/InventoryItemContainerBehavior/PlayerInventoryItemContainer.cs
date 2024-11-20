using System.ComponentModel;
using _Game.Scripts.Shop.EventBusEvents;
using _Game.Scripts.Shop.Items;
using _Game.Scripts.Shop.WalletSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using Utility.EventBusSystem.EventBus;

namespace _Game.Scripts.Shop.InventorySystem.InventoryItemContainerBehavior
{
    public class PlayerInventoryItemContainer : InventoryItemContainer
    {
        public override void OnItemDrop(PointerEventData eventData)
        {
            var inventoryItemContainer = eventData.pointerDrag.GetComponent<InventoryItemContainer>() as ShopInventoryItemContainer;
            
            if (inventoryItemContainer == null || inventoryItemContainer.Item == null)
            {
                throw new WarningException("You cannot drop an item here!");
            }

            if (CheckMoneyToBuy(inventoryItemContainer.Item))
            {
                EventBus.Dispatch(new OnItemBuy(inventoryItemContainer.Item));
                EventBus.Dispatch(new DebugInventoryСontent());
            }
            else
            {
                Debug.LogWarning("You don't have enough money to buy this item!");
            }
        }

        private bool CheckMoneyToBuy(ItemData item)
        {
            if (DIInstaller.Instance.RequireInstanceComponent<WalletController>().MoneyInWallet >= item.ItemPrice)
            {
                return true;
            }

            return false;
        }
    }
}