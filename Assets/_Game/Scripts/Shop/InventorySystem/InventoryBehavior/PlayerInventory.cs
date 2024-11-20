using _Game.Scripts.Shop.EventBusEvents;
using Utility.EventBusSystem.EventBus;

namespace _Game.Scripts.Shop.InventorySystem.InventoryBehavior
{
    public class PlayerInventory : Inventory
    {
        public override void Start()
        {
            base.Start();
        }

        private void OnEnable()
        {
            _subscriptions
                .Add(EventBus.Subscribe<OnItemBuy>(x => AddItem(x.Item)))
                .Add(EventBus.Subscribe<OnItemSold>(x => RemoveItem(x.Item)));
        }

        private void OnDisable()
        {
            _subscriptions.UnsubscribeAll();
        }
    }
}