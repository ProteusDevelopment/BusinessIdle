using System;
using UnityEngine;

namespace Common
{
    public class DontDestroyObjects : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}