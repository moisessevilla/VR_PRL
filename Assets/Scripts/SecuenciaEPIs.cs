using UnityEngine;
using TMPro;

public class SecuenciaEPIs : MonoBehaviour
{
    public static SecuenciaEPIs Instancia;

    [Header("Orden de la secuencia")]
    public string[] orden = { "Chaleco", "Casco", "Gafas", "Earmuff", "Guantes", "Botas" };
    private int pasoActual = 0;

    [Header("Referencias")]
    public TextMeshProUGUI texto;
    public AudioSource sonidoOK;
    public AudioSource sonidoError;
    public AudioSource sonidoWin;

    [Header("Colores del texto")]
    public Color colorCorrecto = new Color(0f, 0.4f, 0f); // verde oscuro
    public Color colorError = Color.red;

    [Header("Puerta")]
    public Transform puertaTransform;
    public GameObject cartel;

    void Awake()
    {
        Instancia = this;
    }

    void Start()
    {
        texto.color = colorCorrecto;
        texto.text = "Empezamos, primero busca y colócate: " + orden[pasoActual];
    }

    public void IntentarColocar(string epiId, GameObject objeto)
    {
        if (pasoActual >= orden.Length) return;

        if (epiId == orden[pasoActual])
        {
            texto.color = colorCorrecto;
            objeto.SetActive(false);
            pasoActual++;

            if (pasoActual >= orden.Length)
            {
                texto.text = "Equipo completo. Ya puedes salir del vestuario";
                cartel.SetActive(false);
                puertaTransform.localEulerAngles = new Vector3(0, 90, 0);
                sonidoWin.Play();
            }
            else
            {
                sonidoOK.Play();
                texto.text = "Busca y colócate: " + orden[pasoActual];
            }
        }
        else
        {
            sonidoError.Play();
            texto.color = colorError;
            texto.text = "Orden incorrecto. Busca primero: " + orden[pasoActual];
        }
    }
}