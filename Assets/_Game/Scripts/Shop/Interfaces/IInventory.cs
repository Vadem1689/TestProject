using _Game.Scripts.Shop.Items;

namespace _Game.Scripts.Shop.Interfaces
{
    public interface IInventory
    {
         void AddItem(ItemData item);
         void RemoveItem(ItemData item);
    }
}