using _Game.Scripts.Shop.EventBusEvents;
using UnityEngine;
using Utility.EventBusSystem.EventBus;
using Utility.EventBusSystem.Subscription;

namespace _Game.Scripts.Shop.WalletSystem
{
    public class WalletController : MonoBehaviour
    {
        [SerializeField] private int _moneyInStart = 100;
        
        [SerializeField] private WalletView _walletView;
        
        private Wallet _wallet;

        private readonly Subscriptions _subscriptions = new();

        public int MoneyInWallet => _wallet.Money;

        private void Start()
        {
            _wallet = new Wallet(_moneyInStart);
            UpdateView();
        }

        private void OnEnable()
        {
            _subscriptions
                .Add(EventBus.Subscribe<OnItemBuy>(x => _wallet.ReduceMoney(x.Item.ItemPrice)))
                .Add(EventBus.Subscribe<OnItemSold>(x => _wallet.AddMoney((int) Mathf.Round(x.Item.PercentageOfPriceDroppedForPlayer/100 * x.Item.ItemPrice))))
                .Add(EventBus.Subscribe<OnMoneyChanged>(UpdateView));
        }

        private void UpdateView()
        {
            _walletView.UpdateMoney(_wallet.Money);
        }

        private void OnDisable()
        {
            _subscriptions.UnsubscribeAll();
        }
    }
}