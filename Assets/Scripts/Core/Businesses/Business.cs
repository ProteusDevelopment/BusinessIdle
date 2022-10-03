using System;
using System.Collections.Generic;
using System.Linq;
using Core.Businesses.Configs;
using Core.Saves;
using UI.Businesses.Holders;
using UnityEngine;

namespace Core.Businesses
{
    public class Business : MonoBehaviour
    {
        private int _saveId;
        private int _currentLevel = 0;
        private int _currentIncome = 0;
        private int _currentLevelUpPrice = 0;

        private float _currentIncomeProgress = 0f;

        private List<BusinessUpgrade> _upgrades = new List<BusinessUpgrade>();

        private BusinessHolder _businessHolder;
        private BusinessConfig _businessConfig;

        public bool IsBought => _currentLevel > 0;

        private void OnEnable()
        {
            PlayerWallet.Instance.OnMoneyChanged += CheckLevelUpAvailable;
        }

        private void OnDisable()
        {
            PlayerWallet.Instance.OnMoneyChanged -= CheckLevelUpAvailable;
        }

        private void Update()
        {
            if (_currentLevel == 0)
                return;

            _currentIncomeProgress += Time.deltaTime;
            UpdateIncomeView();

            if (_currentIncomeProgress >= _businessConfig.incomeDelay)
            {
                _currentIncomeProgress -= _businessConfig.incomeDelay;
                GetIncome();
            }
        }

        private void CheckLevelUpAvailable(int money)
        {
            _businessHolder.SetLevelUpButtonInteractable(money >= _currentLevelUpPrice);
        }

        public void SetData(BusinessConfig businessConfig, int saveId)
        {
            _saveId = saveId;

            _businessHolder = GetComponent<BusinessHolder>();
            _businessHolder.SetData(businessConfig, BuyLevelUp);
            _businessConfig = businessConfig;

            LoadOrCreateSavedData();

            SetUpgrades();

            CalculateCurrentValues();
        }

        private void LoadOrCreateSavedData()
        {
            if (!SaveManager.ExistsSaves())
            {
                SaveManager.Instance.GameSaveData.businesses[_saveId].upgrades =
                    new List<bool>(new bool[_businessConfig.upgrades.Count]);
            }
            else
            {
                _currentIncomeProgress = SaveManager.Instance.GameSaveData.businesses[_saveId].incomeProgress;
                UpdateIncomeView();
            }
        }

        private void SetUpgrades()
        {
            for (var i = 0; i < _businessConfig.upgrades.Count; i++)
            {
                var upgradeIndex = i;
                var businessUpgradeConfig = _businessConfig.upgrades[upgradeIndex];
                var businessUpgrade = _businessHolder.CreateUpgradeElement();
                businessUpgrade.SetData(businessUpgradeConfig, this, () =>
                {
                    CalculateCurrentValues();
                    _businessHolder.UpdateIncomeValue(_currentIncome);
                    SaveManager.Instance.GameSaveData.businesses[_saveId].upgrades[upgradeIndex] = true;
                });
                _upgrades.Add(businessUpgrade);

                if (SaveManager.Instance.GameSaveData.businesses[_saveId].upgrades[upgradeIndex])
                    businessUpgrade.SetBoughtState();
            }
        }

        private void BuyLevelUp()
        {
            PlayerWallet.Instance.TryDecreaseMoney(_currentLevelUpPrice);
            LevelUp();
            SaveManager.Instance.GameSaveData.businesses[_saveId].level = _currentLevel;
        }

        public void LevelUp()
        {
            _currentLevel++;

            CalculateCurrentValues();

            _businessHolder.UpdateLevel(_currentLevel);
            _businessHolder.UpdateIncomeValue(_currentIncome);
            _businessHolder.UpdateLevelUpPrice(_currentLevelUpPrice);
        }

        private void CalculateCurrentValues()
        {
            _currentIncome = Mathf.RoundToInt(_currentLevel * _businessConfig.baseIncome *
                                              (1f + _upgrades.Sum(upgrade => upgrade.IncomeRatio)));
            _currentLevelUpPrice = (_currentLevel + 1) * _businessConfig.basePrice;
        }

        private void GetIncome()
        {
            PlayerWallet.Instance.IncreaseMoney(_currentIncome);
        }

        private void UpdateIncomeView()
        {
            _businessHolder.UpdateIncomeProgress(_currentIncomeProgress / _businessConfig.incomeDelay);
            SaveManager.Instance.GameSaveData.businesses[_saveId].incomeProgress = _currentIncomeProgress;
        }
    }
}