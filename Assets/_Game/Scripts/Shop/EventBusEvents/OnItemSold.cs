using _Game.Scripts.Shop.Items;
using Utility.EventBusSystem.Interfaces;

namespace _Game.Scripts.Shop.EventBusEvents
{
    public class OnItemSold : IEvent
    {
        public readonly ItemData Item;

        public OnItemSold(ItemData item)
        {
            Item = item;
        }
    }
}