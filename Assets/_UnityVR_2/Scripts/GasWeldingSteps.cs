using UnityEngine;

namespace _UnityVR_2.Scripts
{
    public class GasWeldingSteps : MonoBehaviour
    {
        [Header("Step 1: Remove Items")]
        [SerializeField] private bool step1Active;
        [SerializeField] private GameObject elements;

        [Header("Step 2: Place Welder")]
        [SerializeField] private bool step2Active;
        [SerializeField] private GameObject gasWelder;
        [SerializeField] private GameObject ghost;

        [Header("Step 3: Weld Piece")]
        [SerializeField] private bool step3Active;

        private int _currentStep;
        private int _removeCount;
        private bool _welderActivated;
        private bool _isWelded;
        private GameObject _gasBeam;
        private GameObject _weldingSpace;
        private GameObject _weldingLine;
        private Camera _camera;
        private bool _isCameraNotNull;
        private BoxCollider _boxCollider;

        private void Awake()
        {
            _currentStep = 0;
            _removeCount = 0;
            _welderActivated = false;
            _isWelded = false;
            _gasBeam = GameObject.Find("GasBeamFX");
            _weldingSpace = GameObject.Find("WeldingSpace");
            _weldingLine = GameObject.Find("WeldingLine");
        }
    
        private void Start()
        {
            _camera = Camera.main;
            _isCameraNotNull = _camera != null;
            _boxCollider = gasWelder.GetComponent<BoxCollider>();
        }

        private void OnEnable()
        {
            StepList.OnNextStep += SelectStep;
        }

        private void Update()
        {
            switch (_currentStep)
            {
                case 1:
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (_isCameraNotNull)
                        {
                            var ray = _camera.ScreenPointToRay(Input.mousePosition);

                            if (Physics.Raycast(ray, out var hit, 100f))
                            {
                                if (hit.transform.CompareTag("Remove"))
                                {
                                    hit.transform.gameObject.SetActive(false);
                                    _removeCount++;
                                }
                            }
                        }
                    }

                    break;
                }
                case 2:
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (_isCameraNotNull)
                        {
                            var ray = _camera.ScreenPointToRay(Input.mousePosition);

                            if (Physics.Raycast(ray, out var hit, 100f))
                            {
                                if (hit.transform.CompareTag("Place"))
                                {
                                    gasWelder.transform.position = ghost.transform.position;
                                    gasWelder.transform.rotation = ghost.transform.rotation;
                                    _boxCollider.enabled = false;
                                    ghost.SetActive(false);
                                }
                                if (hit.transform.CompareTag("Button"))
                                {
                                    _gasBeam.SetActive(true);
                                    _welderActivated = true;
                                }
                            }
                        }
                    }

                    break;
                }
                case 3:
                {
                    if (Input.GetMouseButton(0))
                    {
                        if (_isCameraNotNull)
                        {
                            var ray = _camera.ScreenPointToRay(Input.mousePosition);

                            if (Physics.Raycast(ray, out var hit, 100f))
                            {
                                if (hit.transform.CompareTag("Place")) 
                                {
                                    var mousePos = Input.mousePosition;
                                    const float distance = 1.3f;
                                    var gasWelderPosition = gasWelder.transform.position;
                                    var xPos = gasWelderPosition.x;
                                    var zPos = gasWelderPosition.z;
                                    gasWelderPosition = _camera.ScreenToWorldPoint(new Vector3(xPos, mousePos.y, distance));
                                    gasWelderPosition = new Vector3(xPos, gasWelderPosition.y, zPos);
                                    gasWelder.transform.position = gasWelderPosition;

                                    var weldingSpacePosition = _weldingSpace.transform.position;
                                    var xPos2 = weldingSpacePosition.x;
                                    var zPos2 = weldingSpacePosition.z;
                                    weldingSpacePosition = _camera.ScreenToWorldPoint(new Vector3(xPos2, mousePos.y, distance));
                                    weldingSpacePosition = new Vector3(xPos2, weldingSpacePosition.y - 0.2f, zPos2);
                                    _weldingSpace.transform.position = weldingSpacePosition;

                                    switch (gasWelderPosition.y)
                                    {
                                        case < 1.22f:
                                            gasWelder.transform.position = new Vector3(xPos, 1.22f, zPos);
                                            _weldingSpace.transform.position = new Vector3(xPos2, 1.22f - 0.2f, zPos2);
                                            break;
                                        case > 1.42f:
                                            gasWelder.transform.position = new Vector3(xPos, 1.42f, zPos);
                                            _weldingSpace.transform.position = new Vector3(xPos2, 1.42f - 0.2f, zPos2);
                                            _isWelded = true;
                                            break;
                                    }
                                }
                            } 
                        }
                    }

                    break;
                }
            }
        }

        private void DisableAll()
        {
            elements.SetActive(false);
            ghost.SetActive(false);
            _gasBeam.SetActive(false);
            _weldingSpace.SetActive(false);
            _weldingLine.SetActive(false);
            gasWelder.GetComponent<BoxCollider>().enabled = false;
        }

        private void SelectStep(int step)
        {
            switch (step)
            {
                case 0:
                    ActivateStep1();
                    break;
                case 1:
                    ActivateStep2();
                    break;
                case 2:
                    ActivateStep3();
                    break;
                case 3:
                    ActivateEnd();
                    break;
            }
        }

        private void ActivateStep1()
        {
            if (!step1Active)
            {
                return;
            }

            DisableAll();
            elements.SetActive(true);
            _currentStep = 1;
        }

        private void ActivateStep2()
        {
            if (_removeCount == 3)
            {
                Debug.Log("¡Objetos peligrosos eliminados con éxito!");
            }
            else
            {
                Debug.LogWarning("¡No has eliminado todos los objetos peligrosos!");
            }

            if (!step2Active)
            {
                return;
            }

            DisableAll();
            ghost.SetActive(true);
            gasWelder.GetComponent<BoxCollider>().enabled = true;
            _currentStep = 2;
        }

        private void ActivateStep3()
        {
            if (_welderActivated)
            {
                Debug.Log("¡Soldador activado con éxtio!");
            }
            else
            {
                Debug.LogWarning("¡No has activado el soldador!");
            }

            if (!step3Active)
            {
                return;
            }

            DisableAll();
            _gasBeam.SetActive(true);
            _weldingSpace.SetActive(true);
            _weldingLine.SetActive(true);
            gasWelder.GetComponent<BoxCollider>().enabled = true;
            _currentStep = 3;
        }

        private void ActivateEnd()
        {
            if (_isWelded)
            {
                Debug.Log("¡Pieza soldada con éxtio!");
            }
            else
            {
                Debug.LogWarning("¡No has soldado la pieza!");
            }

            DisableAll();
            _currentStep = 4;

            UnityEditor.EditorApplication.isPlaying = false;
        }

        private void OnDisable()
        {
            StepList.OnNextStep -= SelectStep;
        }
    }
}