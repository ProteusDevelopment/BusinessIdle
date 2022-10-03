using System.Collections.Generic;
using UnityEngine;

namespace Core.Businesses.Configs
{
    [CreateAssetMenu(fileName = "BusinessesOrder", menuName = "Businesses/BusinessesOrder", order = 1)]
    public class BusinessesOrderConfig : ScriptableObject
    {
        public List<BusinessConfig> businesses;
    }
}