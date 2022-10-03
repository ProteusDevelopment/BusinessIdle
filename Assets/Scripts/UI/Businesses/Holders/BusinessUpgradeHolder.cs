using System;
using Core.Businesses.Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Businesses.Holders
{
    public class BusinessUpgradeHolder : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _incomeValue;
        [SerializeField] private TMP_Text _price;
        [SerializeField] private Button _buy;

        public void SetData(BusinessUpgradeConfig businessUpgradeConfig, Action onBuyButtonClicked)
        {
            _name.text = businessUpgradeConfig.name;
            _incomeValue.text = $"Доход: + {(int)(businessUpgradeConfig.incomeRatio * 100f)}%";
            _price.text = $"Цена: {businessUpgradeConfig.price}$";
            
            _buy.onClick.AddListener(onBuyButtonClicked.Invoke);
        }

        public void SetBoughtState()
        {
            _price.text = "Куплено";
            SetBuyButtonInteractable(false);
        }
        
        public void SetBuyButtonInteractable(bool interactable)
        {
            _buy.interactable = interactable;
        }
    }
}