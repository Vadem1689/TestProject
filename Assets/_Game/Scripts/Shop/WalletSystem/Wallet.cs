using System;
using _Game.Scripts.Shop.EventBusEvents;
using Utility.EventBusSystem.EventBus;

namespace _Game.Scripts.Shop.WalletSystem
{
    public class Wallet 
    {
        private int _money;
        public int Money => _money;
        
        public Wallet(int moneyInStart)
        {
            _money = moneyInStart;
        }

        public void AddMoney(int amount)
        {
            if (amount <= 0)
            {
                throw new Exception("Amount must be greater than 0");
            }
            
            _money += amount;
            
            EventBus.Dispatch(new OnMoneyChanged());
        }

        public void ReduceMoney(int amount)
        {
            if (amount <= 0)
            {
                throw new Exception("Amount must be greater than 0");
            }
            
            _money -= amount;
            
            EventBus.Dispatch(new OnMoneyChanged());
        }
    }
}
