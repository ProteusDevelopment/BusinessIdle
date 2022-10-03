using Core;
using TMPro;
using UnityEngine;

namespace UI
{
    public class PlayerWalletUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _money;
        
        private void OnEnable()
        {
            PlayerWallet.Instance.OnMoneyChanged += UpdateMoney;
        }
        
        private void OnDisable()
        {
            PlayerWallet.Instance.OnMoneyChanged -= UpdateMoney;
        }

        private void UpdateMoney(int value)
        {
            _money.text = $"Баланс: {value}$";
        }
    }
}