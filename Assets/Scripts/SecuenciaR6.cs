using System.Collections;
using UnityEngine;
using TMPro;

public class SecuenciaR6 : MonoBehaviour
{
    public static SecuenciaR6 Instancia;

    [Header("HUD")]
    public TextMeshProUGUI texto;
    public AudioSource sonidoBeep;
    public GameObject panelHUD;

    [Header("Jugador y Teletransporte")]
    public Transform jugador;
    public Transform raizJugador;
    public Transform raizCamara;
    public Behaviour locomotor;

    [Header("Sala Normal")]
    public Rigidbody botellaGas;
    public GameObject humoGas;
    public GameObject evanescencia;
    public GameObject mascaraGas;
    public GameObject puertaBloqueo;
    public Transform baseBotella;
    public Transform baseNormal;
    public Transform baseInicioDestino;
    public AudioSource sonidoBotella;
    public AudioSource sonidoGas;
    public AudioSource sonidoTokTok;

    [Header("Sala Clon")]
    public Transform baseCloneInicio;
    public Transform baseClon1;
    public Transform baseClon2;
    public GameObject gasDimensional;
    public GameObject mascaraGasClon;
    public GameObject puertaDimensionalNormal;

    [Header("Sala Matrix")]
    public Transform baseMatrixInicio;
    public Transform baseMatrixSillon;
    public Transform baseMatrixMIB;
    public AudioSource sonidoCaida;
    public AudioSource sonidoMatrix;
    public AudioSource sonidoBang;

    [Header("Sonidos Generales")]
    public AudioSource sonidoOK;
    public AudioSource sonidoIN;
    public AudioSource sonidoOUT;

    [Header("Pantalla Final")]
    public GameObject pantallaFinal;
    public TextMeshProUGUI textoFinal;

    private string eventoRecibido = "";
    private const float radioZona = 1.5f;

    void Awake()
    {
        Instancia = this;
    }

    void Start()
    {
        StartCoroutine(Secuencia());
    }

    public void ProcesarEvento(string evento)
    {
        eventoRecibido = evento;
    }

    IEnumerator EsperarEvento(string esperado)
    {
        eventoRecibido = "";
        while (eventoRecibido != esperado)
        {
            yield return null;
        }
    }

    IEnumerator EsperarLlegada(Transform destino)
    {
        while (Vector3.Distance(jugador.position, destino.position) > radioZona)
        {
            yield return null;
        }
    }

    IEnumerator EsperarCaidaBotella()
    {
        while (Vector3.Distance(botellaGas.transform.position, baseBotella.position) > 3f)
        {
            yield return null;
        }
    }

    void Mensaje(string msg)
    {
        texto.text = msg;
        sonidoBeep.Play();
    }

    void Teletransportar(Transform destino)
    {
        locomotor.enabled = false;
        raizJugador.position = destino.position;
        locomotor.enabled = true;
    }

    IEnumerator Secuencia()
    {
        yield return EsperarLlegada(baseInicioDestino);

        yield return new WaitForSeconds(3f);

        botellaGas.useGravity = true;
        botellaGas.isKinematic = false;

        yield return EsperarCaidaBotella();

        sonidoBotella.Play();
        yield return new WaitForSeconds(sonidoBotella.clip.length);

        humoGas.SetActive(true);
        sonidoGas.Play();
        yield return new WaitForSeconds(3f);
        sonidoGas.Stop();

        yield return new WaitForSeconds(1f);

        evanescencia.SetActive(true);
        puertaBloqueo.SetActive(true);

        Mensaje("Riesgo actualizado, se ha encontrado riesgo de intoxicación, abandona la sala inmediatamente");

        yield return EsperarLlegada(puertaBloqueo.transform);

        sonidoTokTok.Play();
        yield return new WaitForSeconds(sonidoTokTok.clip.length);

        Mensaje("Rápido, busca una máscara de gas, peligro de muerte inminente!");

        yield return new WaitForSeconds(4f);

        Mensaje("Máscara de gas inaccesible en el techo, busca una salida alternativa.");

        mascaraGas.SetActive(true);
        yield return EsperarLlegada(baseNormal);

        sonidoIN.Play();
        gasDimensional.SetActive(true);
        Teletransportar(baseCloneInicio);

        yield return EsperarLlegada(baseClon1);

        sonidoOUT.Play();
        Teletransportar(baseInicioDestino);

        yield return EsperarLlegada(baseNormal);

        sonidoIN.Play();
        Teletransportar(baseCloneInicio);

        yield return EsperarEvento("MascaraCogida");

        sonidoOK.Play();
        yield return new WaitForSeconds(3f);
        mascaraGasClon.SetActive(false);

        Mensaje("Perfecto, máscara de gas colocada, busca una salida para terminar el riesgo de intoxicación");

        yield return EsperarLlegada(baseClon2);

        sonidoOUT.Play();
        puertaBloqueo.SetActive(false);
        puertaDimensionalNormal.SetActive(true);
        mascaraGas.SetActive(false);
        Teletransportar(baseNormal);

        yield return EsperarLlegada(puertaDimensionalNormal.transform);

        sonidoIN.Play();
        Teletransportar(baseMatrixInicio);

        panelHUD.SetActive(false);

        sonidoCaida.Play();
        yield return EsperarLlegada(baseMatrixSillon);

        panelHUD.SetActive(true);
        sonidoCaida.Stop();
        sonidoMatrix.Play();
        Mensaje("Alguna vez has tenido un sueño que parecia tan real, que no podías distinguirlo de la realidad?");

        yield return EsperarLlegada(baseMatrixMIB);

        sonidoBang.Play();
        panelHUD.SetActive(false);
        yield return new WaitForSeconds(0.7f);
        pantallaFinal.SetActive(true);
        textoFinal.text = "Simulación Finalizada\n" +
                           "TFM VIU - Máster en Industria 4.0\n" +
                           "Alumno: Moisés Sevilla Corrales\n" +
                           "Tutor: Salva Aparici Martínez\n" +
                           "Curso 2025-2026";
    }
}