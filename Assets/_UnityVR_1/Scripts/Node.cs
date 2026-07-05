using UnityEngine;

namespace _UnityVR_1.Scripts
{
    public class Node : MonoBehaviour
    {
        private Canvas _canvas;
        protected DataAccess Data;

        protected virtual void Start()
        {
            _canvas = GetComponentInChildren<Canvas>();
            _canvas.worldCamera = Camera.main;

            Data = FindFirstObjectByType<DataAccess>();

            if (Data == null)
            {
                Debug.Log("Añade el objeto DataAccess a la escena.");
            }
        }
    }
}