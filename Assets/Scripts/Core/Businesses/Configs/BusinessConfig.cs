using System.Collections.Generic;
using UnityEngine;

namespace Core.Businesses.Configs
{
    [CreateAssetMenu(fileName = "Business", menuName = "Businesses/Business", order = 0)]
    public class BusinessConfig : ScriptableObject
    {
        public string businessName;
        public float incomeDelay;
        public int basePrice;
        public int baseIncome;
        public List<BusinessUpgradeConfig> upgrades;
    }
}