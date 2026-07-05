using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _UnityVR_2.Scripts
{
    public class StepList : MonoBehaviour
    {
        public List<Step> steps;

        private Button _nextStepButton;
        private TMP_Text _stepTitle;
        private TMP_Text _stepText;

        private int _currentStep;
        private bool _end;

        public delegate void NextStep(int step);
        public static event NextStep OnNextStep;

        private void Start()
        {
            _nextStepButton = GameObject.Find("NextButton").GetComponent<Button>();
            _stepTitle = GameObject.Find("StepTitle").GetComponent<TMP_Text>();
            _stepText = GameObject.Find("StepText").GetComponent<TMP_Text>();

            _nextStepButton.onClick.AddListener(OnNextPressed);
        
            for (var i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            SelectStep(_currentStep);
            UpdateButtons();
        }

        private void SelectStep(int index)
        {
            if (steps.Count == 0)
            {
                _stepTitle.text = "No hay pasos";
                _stepTitle.color = Color.red;
                _stepText.text = "Añade pasos desde StepList haciendo click en Add Step";
                _stepText.color = Color.red;
                return;
            }

            foreach (var step in steps)
            {
                step.gameObject.SetActive(false);
            }

            steps[index].gameObject.SetActive(true);
            _stepTitle.text = steps[index].title;
            _stepText.text = steps[index].text;

            OnNextStep?.Invoke(index);
        }

        private void UpdateButtons()
        {
            _nextStepButton.gameObject.SetActive(true);

            if (steps.Count <= 1 || _currentStep == steps.Count - 1)
            {
                _end = true;
            }
        }

        private void OnNextPressed()
        {
            if (_end)
            {
                OnNextStep?.Invoke(3);
            }
            else
            {
                _currentStep = Mathf.Min(++_currentStep, steps.Count - 1);
                SelectStep(_currentStep);
                UpdateButtons();
            }
        }
    }
}