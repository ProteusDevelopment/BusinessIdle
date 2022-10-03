using System;
using System.Collections.Generic;
using Core.Businesses;
using Core.Businesses.Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Businesses.Holders
{
    public class BusinessHolder : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private Image _incomeProgress;
        [SerializeField] private TMP_Text _levelValue;
        [SerializeField] private TMP_Text _incomeValue;
        [SerializeField] private Button _levelUp;
        [SerializeField] private TMP_Text _levelUpPrice;
        [SerializeField] private Transform _upgradesParent;

        public void SetData(BusinessConfig businessConfig, Action onLevelUpButtonClicked)
        {
            _name.text = businessConfig.businessName;
            UpdateIncomeProgress(0f);
            UpdateLevel(0);
            UpdateIncomeValue(businessConfig.baseIncome);
            _levelUp.onClick.AddListener(onLevelUpButtonClicked.Invoke);
            UpdateLevelUpPrice(businessConfig.basePrice);
        }

        public BusinessUpgrade CreateUpgradeElement()
        {
            var businessUpgrade = Instantiate(
                Resources.Load<BusinessUpgrade>("Prefabs/Businesses/BusinessUpgradeHolder"),
                _upgradesParent);
            return businessUpgrade;
        }

        public void UpdateIncomeProgress(float amount)
        {
            _incomeProgress.fillAmount = amount;
        }

        public void UpdateLevel(int level)
        {
            _levelValue.text = level.ToString();
        }

        public void UpdateIncomeValue(int income)
        {
            _incomeValue.text = $"{income}$";
        }

        public void SetLevelUpButtonInteractable(bool interactable)
        {
            _levelUp.interactable = interactable;
        }

        public void UpdateLevelUpPrice(int price)
        {
            _levelUpPrice.text = $"Цена: {price}$";
        }
    }
}
