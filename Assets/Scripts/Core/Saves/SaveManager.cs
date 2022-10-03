using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Common;
using Core.Businesses.Generator;
using UnityEngine;

namespace Core.Saves
{
    public class SaveManager : Singleton<SaveManager>
    {
        [SerializeField] private BusinessesGenerator _businessesGenerator;
        
        public GameSaveData GameSaveData { get; private set; } = new GameSaveData();

        protected override void Awake()
        {
            base.Awake();
            
            LoadFromFile();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                SaveToFile();
            }
            else
            {
                LoadFromFile();
            }
        }

        private void OnApplicationQuit()
        {
            SaveToFile();
        }

        private void OnEnable()
        {
            PlayerWallet.Instance.OnMoneyChanged += SavePlayerWallet;
        }

        private void OnDisable()
        {
            PlayerWallet.Instance.OnMoneyChanged -= SavePlayerWallet;
        }

        private void SaveToFile()
        {
            var binaryFormatter = new BinaryFormatter();
            var fileStream = File.Create(Application.persistentDataPath + "/save.sv");
            binaryFormatter.Serialize(fileStream, GameSaveData);
            fileStream.Close();
        }

        public void LoadFromFile()
        {
            if (!ExistsSaves())
                return;
            
            var binaryFormatter = new BinaryFormatter();
            var fileStream = File.Open(Application.persistentDataPath + "/save.sv", FileMode.Open);
            GameSaveData = binaryFormatter.Deserialize(fileStream) as GameSaveData;
            fileStream.Close();
        }

        public static bool ExistsSaves()
        {
            return File.Exists(Application.persistentDataPath + "/save.sv");
        }

        private void SavePlayerWallet(int money)
        {
            GameSaveData.playerWallet = money;
        }
    }
}