using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _UnityVR_1.Scripts
{
    public class DataAccess : MonoBehaviour
    {
        public DataSet data;

        private Dictionary<string, Datum> _dataDictionary;

        private void Awake()
        {
            _dataDictionary = new Dictionary<string, Datum>();

            foreach (var d in data.data)
            {
                if (string.IsNullOrWhiteSpace(d.name)) continue;
                _dataDictionary.Add(d.name, d);
                d.CurrentValue = d.initialValue;
            }
        }

        private void Start()
        {
            StartCoroutine(Period());
        }

        private IEnumerator Period()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);

                foreach (var v in _dataDictionary.Values)
                {
                    v.CurrentValue += v.variation;
                    v.CurrentValue = Mathf.Clamp(v.CurrentValue, 0, 10);
                    v.Update();
                }
            }
        }

        public Datum Get(string dataName)
        {
            return _dataDictionary.GetValueOrDefault(dataName);
        }
    }
}