using System;
using UnityEngine;

namespace _UnityVR_1.Scripts
{
    [Serializable]
    public class Datum
    {
        public string name;
        [Range(0, 10)] public float initialValue;
        public float variation;

        public delegate void OnUpdateEvent();
        public event OnUpdateEvent OnUpdate;

        public float CurrentValue { get; set; }

        public void Update()
        {
            OnUpdate?.Invoke();
        }

        public void Reset()
        {
            CurrentValue = initialValue;
            Update();
        }
    }
}