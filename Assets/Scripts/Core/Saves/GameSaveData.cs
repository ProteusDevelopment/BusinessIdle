using System.Collections.Generic;
using UnityEngine;

namespace Core.Saves
{
    [System.Serializable]
    public class GameSaveData
    {
        public int playerWallet;
        public List<BusinessSaveData> businesses;
    }
}