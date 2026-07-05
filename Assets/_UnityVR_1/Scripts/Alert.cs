using UnityEngine;
using UnityEngine.UI;

namespace _UnityVR_1.Scripts
{
    public class Alert : MonoBehaviour
    {
        public Info info;

        public bool hasMin;
        public float minValue;

        public bool hasMax;
        public float maxValue = 10;

        public Color normalColor = Color.green;
        public Color warningColor = Color.red;

        private Image _image;

        protected void Start()
        {
            _image = GetComponentInChildren<Image>();
            _image.color = normalColor;

            if (info != null)
            {
                info.OnUpdate += OnUpdate;
            }
            else
            {
                Debug.LogWarning(gameObject.name + " no tiene ningún objeto Info asignado.");
            }
        }

        public void OnValidate()
        {
            _image = GetComponentInChildren<Image>();
        
            if (_image != null)
            {
                _image.color = normalColor;
            }
        }

        private void OnUpdate(float value)
        {
            var color = normalColor;

            if (hasMax && value >= maxValue)
            {
                color = warningColor;
            }
            else if (hasMin && value <= minValue)
            {
                color = warningColor;
            }

            _image.color = color;
        }
    }
}