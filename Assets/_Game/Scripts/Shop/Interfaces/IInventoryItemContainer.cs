using UnityEngine.EventSystems;

namespace _Game.Scripts.Interfaces
{
    public interface IInventoryItemContainer
    {
        public void OnItemDrop(PointerEventData eventData);
    }
}