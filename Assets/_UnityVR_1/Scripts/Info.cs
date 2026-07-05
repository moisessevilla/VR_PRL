using TMPro;
using UnityEngine;

namespace _UnityVR_1.Scripts
{
    public class Info : Node
    {
        public string dataName;

        [Header("UI")]
        [Space]
        public TMP_Text nameText;
        public TMP_Text valueText;

        private Datum _datum;

        public delegate void OnUpdateEvent(float value);
        public event OnUpdateEvent OnUpdate;

        protected override void Start()
        {
            base.Start();

            if (Data == null)
            {
                return;
            }

            if (!string.IsNullOrWhiteSpace(dataName))
            {
                _datum = Data.Get(dataName);

                if (_datum != null)
                {
                    _datum.OnUpdate += DataUpdate;
                    nameText.text = dataName;
                    valueText.text = _datum.initialValue.ToString("N2");
                }
                else
                {
                    Debug.LogWarning("No hay ningún dato llamado " + dataName);
                }
            }
            else
            {
                Debug.LogWarning(gameObject.name + " no tiene ningun dato asignado.");
            }
        }

        private void DataUpdate()
        {
            valueText.text = _datum.CurrentValue.ToString("N2");
            OnUpdate?.Invoke(_datum.CurrentValue);
        }

        public void ResetValue()
        {
            _datum?.Reset();
        }
    }
}