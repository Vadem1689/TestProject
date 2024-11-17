using _Game.Scripts.Shop.InventorySystem;
using _Game.Scripts.Shop.WalletSystem;
using DI;
using UnityEngine;
using Utility;

namespace _Game.Scripts.Shop
{
    public class DIInstaller : SingletonBehavior<DIInstaller>
    {
        [SerializeField] private Canvas _mainCanvas;
        [SerializeField] private ItemPlaceHolder _itemPlaceHolder;
        [SerializeField] private WalletController _walletController;
        
        private DIСontainer _container;
        
        private void Awake()
        {
            _container = new DIСontainer();
            
            _container.RegisterInstance(_mainCanvas);
            _container.RegisterInstance(_itemPlaceHolder);
            _container.RegisterInstance(_walletController);
        }

        public T RequireInstanceComponent<T>()
        {
            return _container.Resolve<T>();
        }
    }
}