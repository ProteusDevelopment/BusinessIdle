using System.Collections.Generic;
using Core.Businesses.Configs;
using UnityEditor;
using UnityEngine;

namespace Editor.Businesses.Configs
{
    [CustomEditor(typeof(BusinessesOrderConfig))]
    public class BusinessesOrderConfigEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Load"))
            {
                var config = serializedObject.targetObject as BusinessesOrderConfig;

                config.businesses = new List<BusinessConfig>();
                config.businesses.AddRange(
                    Resources.LoadAll<BusinessConfig>("Configs/Businesses"));
            }
        
            base.OnInspectorGUI();
        }
    }
}