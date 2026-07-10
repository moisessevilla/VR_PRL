using UnityEngine;
using TMPro;

public class SecuenciaRiesgos : MonoBehaviour
{
    public static SecuenciaRiesgos Instancia;

    [System.Serializable]
    public class Riesgo
    {
        public string id;
        public Transform puntoGuia;
        public string mensaje;
        public int cantidadRequerida = 1;
        public string mensajeInicio;
    }

    public Riesgo[] riesgos;
    private int[] progresoActual;
    private int pasoActual = 0;
    private const float distanciaLlegada = 3f;
    private bool lineaActiva = false;

    [Header("Referencias")]
    public TextMeshProUGUI texto;
    public GameObject panelHUD;
    public AudioSource sonidoBeep;
    public AudioSource sonidoOK;
    public AudioSource sonidoWin;
    public LineRenderer lineaGuia;
    public Transform jugador;

    void Awake()
    {
        Instancia = this;
        progresoActual = new int[riesgos.Length];
    }

    public void IniciarRecorrido()
    {
        panelHUD.SetActive(true);
        pasoActual = 0;
        ActivarRiesgoActual();
    }

    void ActivarRiesgoActual()
    {
        string msg = string.IsNullOrEmpty(riesgos[pasoActual].mensajeInicio) ? "Dirigete al punto marcado" : riesgos[pasoActual].mensajeInicio;
        texto.text = msg;
        sonidoBeep.Play();
        lineaGuia.enabled = true;
        lineaActiva = true;
    }

    void Update()
    {
        if (!lineaActiva || pasoActual >= riesgos.Length) return;

        var objetivo = riesgos[pasoActual].puntoGuia;

        Vector3 inicio = jugador.position;
        inicio.y -= 0.5f;

        Vector3 fin = objetivo.position;
        fin.y += 0.5f;

        lineaGuia.SetPosition(0, inicio);
        lineaGuia.SetPosition(1, fin);

        if (Vector3.Distance(jugador.position, objetivo.position) < distanciaLlegada)
        {
            lineaGuia.enabled = false;
            lineaActiva = false;
            texto.text = riesgos[pasoActual].mensaje;
            sonidoBeep.Play();
        }
    }

    public void ResolverRiesgo(string id)
    {
        if (pasoActual >= riesgos.Length || id != riesgos[pasoActual].id) return;

        progresoActual[pasoActual]++;
        sonidoOK.Play();

        if (progresoActual[pasoActual] < riesgos[pasoActual].cantidadRequerida)
        {
            return;
        }

        pasoActual++;

        if (pasoActual >= riesgos.Length)
        {
            texto.text = "Riesgos superados";
            sonidoWin.Play();
        }
        else
        {
            ActivarRiesgoActual();
        }
    }
}