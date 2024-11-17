using UnityEngine;

namespace _Game.Scripts.Shop.Items
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "NewItemData", order = 0)]
    public class ItemData : ScriptableObject
    {
        [SerializeField] private int _itemPrice;
        [SerializeField] private float _percentageOfPriceDroppedForPlayer;
        [SerializeField] private Sprite _sprite;

        public Sprite Sprite => _sprite;
        public int ItemPrice => _itemPrice;
        public float PercentageOfPriceDroppedForPlayer => _percentageOfPriceDroppedForPlayer;
    }
}