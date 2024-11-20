using System.Collections.Generic;
using _Game.Scripts.Shop.EventBusEvents;
using _Game.Scripts.Shop.Items;
using UnityEngine;
using Utility.EventBusSystem.EventBus;

namespace _Game.Scripts.Shop.InventorySystem.InventoryBehavior
{
    public class ShopInventory : Inventory
    {
        [SerializeField] private List<ItemData> _startShopItem = new List<ItemData>();

        public override void Start()
        {
            base.Start();
            _startShopItem.ForEach(AddItem);
            
            EventBus.Dispatch(new DebugInventoryСontent());
        }
        
        private void OnEnable()
        {
            _subscriptions
                .Add(EventBus.Subscribe<OnItemBuy>(x => RemoveItem(x.Item)))
                .Add(EventBus.Subscribe<OnItemSold>(x => AddItem(x.Item)));
        }

        private void OnDisable()
        {
            _subscriptions.UnsubscribeAll();
        }
    }
}