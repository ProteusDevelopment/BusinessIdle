using System.Collections.Generic;

namespace Core.Saves
{
    [System.Serializable]
    public class BusinessSaveData
    {
        public int level;
        public float incomeProgress;
        public List<bool> upgrades;
    }
}