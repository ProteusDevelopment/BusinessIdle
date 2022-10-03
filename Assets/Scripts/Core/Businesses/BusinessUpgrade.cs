using System;
using Core.Businesses.Configs;
using UI.Businesses.Holders;
using UnityEngine;

namespace Core.Businesses
{
    public class BusinessUpgrade : MonoBehaviour
    {
        private Business _parentBusiness;
        private BusinessUpgradeHolder _businessUpgradeHolder;
        private BusinessUpgradeConfig _businessUpgradeConfig;
        private Action _onBuyAction;

        public float IncomeRatio { get; private set; } = 0f;

        private void OnEnable()
        {
            PlayerWallet.Instance.OnMoneyChanged += CheckBuyAvailable;
        }

        private void OnDisable()
        {
            PlayerWallet.Instance.OnMoneyChanged -= CheckBuyAvailable;
        }

        private void CheckBuyAvailable(int money)
        {
            if (_parentBusiness == null || !_parentBusiness.IsBought)
                return;
            
            _businessUpgradeHolder.SetBuyButtonInteractable(money >= _businessUpgradeConfig.price);
        }
        
        public void SetData(BusinessUpgradeConfig businessUpgradeConfig, Business parentBusiness, Action onBuyAction)
        {
            _businessUpgradeHolder = GetComponent<BusinessUpgradeHolder>();
            _businessUpgradeHolder.SetData(businessUpgradeConfig, Buy);
            
            _businessUpgradeConfig = businessUpgradeConfig;
            _parentBusiness = parentBusiness;
            _onBuyAction = onBuyAction;
        }

        private void Buy()
        {
            PlayerWallet.Instance.TryDecreaseMoney(_businessUpgradeConfig.price);
            SetBoughtState();
        }

        public void SetBoughtState()
        {
            IncomeRatio = _businessUpgradeConfig.incomeRatio;
            _businessUpgradeHolder.SetBoughtState();
            PlayerWallet.Instance.OnMoneyChanged -= CheckBuyAvailable;
            _onBuyAction.Invoke();
        }
    }
}