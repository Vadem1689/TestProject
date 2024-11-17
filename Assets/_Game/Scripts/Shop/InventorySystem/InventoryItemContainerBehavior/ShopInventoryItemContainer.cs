using System;
using _Game.Scripts.Shop.EventBusEvents;
using UnityEngine.EventSystems;
using Utility.EventBusSystem.EventBus;

namespace _Game.Scripts.Shop.InventorySystem.InventoryItemContainerBehavior
{
    public class ShopInventoryItemContainer : InventoryItemContainer
    {
        public override void OnItemDrop(PointerEventData eventData)
        {
            var inventoryItemContainer = eventData.pointerDrag.GetComponent<InventoryItemContainer>() as PlayerInventoryItemContainer;
            
            if (inventoryItemContainer == null)
            {
                throw new Exception("You cannot drop an item here!");
            }
            
            EventBus.Dispatch(new OnItemSold(inventoryItemContainer.Item));
        }
    }
}