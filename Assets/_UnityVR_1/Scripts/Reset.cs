using UnityEngine;
using UnityEngine.UI;

namespace _UnityVR_1.Scripts
{
    public class Reset : Node
    {
        public Info info;

        private Button _button;

        protected override void Start()
        {
            base.Start();

            _button = GetComponentInChildren<Button>();

            if (info != null)
            {
                _button.onClick.AddListener(() => info.ResetValue());
            }
            else
            {
                Debug.LogWarning(gameObject.name + " no tiene ningún objeto Info asignado.");
            }
        }
    }
}