using System;
using _Game.Scripts.Interfaces;
using _Game.Scripts.Shop.Items;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Game.Scripts.Shop.InventorySystem.InventoryItemContainerBehavior
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class InventoryItemContainer : MonoBehaviour,
        IBeginDragHandler,
        IDragHandler,
        IEndDragHandler,
        IDropHandler,
        IInventoryItemContainer
    {
        [SerializeField] private Image _image;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private RectTransform _rectTransform;
        
        private Transform _parentTransform;

        private Canvas _mainCanvas;
        private ItemData _item;

        private ItemPlaceHolder _placeHolder;
        public ItemData Item => _item;


        private void Start()
        {
            _mainCanvas = DIInstaller.Instance.RequireInstanceComponent<Canvas>();
            _placeHolder = DIInstaller.Instance.RequireInstanceComponent<ItemPlaceHolder>();

            _parentTransform = transform.parent;
        }

        public virtual void AddItem(ItemData item)
        {
            if (item == null)
            {
                throw new Exception("Item can't be null!");
            }
            
            _item = item;
            _image.sprite = _item.Sprite;
        }

        public virtual void Clear()
        {
            _item = null;
            _image.sprite = null;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if(_item == null) return;
            
            _canvasGroup.blocksRaycasts = false;
            transform.SetParent(_placeHolder.transform, true);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if(_item == null) return;
            
            _rectTransform.anchoredPosition += eventData.delta / _mainCanvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.SetParent(_parentTransform, false);
            
            _canvasGroup.blocksRaycasts = true;
            transform.localPosition = Vector3.zero;
        }

        public void OnDrop(PointerEventData eventData)
        {
            OnItemDrop(eventData);
        }

        public abstract void OnItemDrop(PointerEventData eventData);

    }
}