using System;
using Common;
using Core.Saves;
using UnityEngine;

namespace Core
{
    public class PlayerWallet : Singleton<PlayerWallet>
    {
        [SerializeField] private int _startMoney = 0;

        private int _money = 0;
        
        public event Action<int> OnMoneyChanged = delegate {  };

        private void Start()
        {
            if (SaveManager.ExistsSaves())
            {
                IncreaseMoney(SaveManager.Instance.GameSaveData.playerWallet);
            }
            else
            {
                IncreaseMoney(_startMoney);
            }
        }

        public void IncreaseMoney(int amount)
        {
            _money += amount;
            OnMoneyChanged.Invoke(_money);
        }

        public bool TryDecreaseMoney(int amount)
        {
            if (_money < amount)
                return false;
            
            _money -= amount;
            OnMoneyChanged.Invoke(_money);
            return true;
        }
    }
}