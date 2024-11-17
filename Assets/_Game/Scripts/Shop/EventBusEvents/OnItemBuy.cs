using _Game.Scripts.Shop.Items;
using Utility.EventBusSystem.Interfaces;

namespace _Game.Scripts.Shop.EventBusEvents
{
    public class OnItemBuy : IEvent
    {
        public readonly ItemData Item;

        public OnItemBuy(ItemData item)
        {
            Item = item;
        }
    }
}