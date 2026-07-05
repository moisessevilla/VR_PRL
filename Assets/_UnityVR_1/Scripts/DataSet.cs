using UnityEngine;

namespace _UnityVR_1.Scripts
{
    [CreateAssetMenu(fileName = "Data", menuName = "DataSet")]
    public class DataSet : ScriptableObject
    {
        public Datum[] data;
    }
}