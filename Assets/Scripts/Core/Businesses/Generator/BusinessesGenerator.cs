using System.Collections.Generic;
using Core.Businesses.Configs;
using Core.Saves;
using UnityEngine;

namespace Core.Businesses.Generator
{
    public class BusinessesGenerator : MonoBehaviour
    {
        [SerializeField] private BusinessesOrderConfig _businessesOrder;
        [SerializeField] private Transform _businessesParent;

        private List<Business> _businesses = new List<Business>();

        private void Awake()
        {
            PrepareToLoad();

            GenerateBusinesses();
            
            LoadLevelsOrCreate();
        }

        private void PrepareToLoad()
        {
            SaveManager.Instance.LoadFromFile();
            if (SaveManager.ExistsSaves())
                return;
            
            SaveManager.Instance.GameSaveData.businesses =
                new List<BusinessSaveData>();
            for (var i = 0; i < _businessesOrder.businesses.Count; i++)
                SaveManager.Instance.GameSaveData.businesses.Add(new BusinessSaveData());
        }

        private void GenerateBusinesses()
        {
            for (var i = 0; i < _businessesOrder.businesses.Count; i++)
            {
                var businessConfig = _businessesOrder.businesses[i];
                var business = Instantiate(Resources.Load<Business>("Prefabs/Businesses/BusinessHolder"),
                    _businessesParent);
                business.SetData(businessConfig, i);
                _businesses.Add(business);
            }
        }

        private void LoadLevelsOrCreate()
        {
            if (SaveManager.ExistsSaves())
            {
                for (var i = 0; i < SaveManager.Instance.GameSaveData.businesses.Count; i++)
                {
                    var business = SaveManager.Instance.GameSaveData.businesses[i];
                    for (var level = 0; level < business.level; level++)
                        _businesses[i].LevelUp();
                }
            }
            else
            {
                _businesses[0].LevelUp();
                SaveManager.Instance.GameSaveData.businesses[0].level = 1;
            }
        }
    }
}