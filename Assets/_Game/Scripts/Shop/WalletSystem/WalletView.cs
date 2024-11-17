using TMPro;
using UnityEngine;

namespace _Game.Scripts.Shop.WalletSystem
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _moneyCounter;

        public void UpdateMoney(int money)
        {
            _moneyCounter.text = "Money: " + money;
        }
    }
}