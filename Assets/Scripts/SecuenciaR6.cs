using System.Collections;
using UnityEngine;
using TMPro;

public class SecuenciaR6 : MonoBehaviour
{
    public static SecuenciaR6 Instancia;

    [Header("HUD")]
    public TextMeshProUGUI texto;
    public AudioSource sonidoBeep;

    [Header("Objetos Sala Normal")]
    public Rigidbody botellaGas;
    public GameObject humoGas;
    public GameObject evanescencia;
    public GameObject mascaraGas;
    public GameObject puertaBloqueo;
    public Transform baseBotella;
    public Behaviour locomotor;
    public AudioSource sonidoOK;
    public GameObject mascaraGasClon;

    [Header("Sonidos Fase 1")]
    public AudioSource sonidoBotella;
    public AudioSource sonidoGas;

    private string eventoRecibido = "";

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

    void Mensaje(string msg)
    {
        texto.text = msg;
        sonidoBeep.Play();
    }

    IEnumerator EsperarCaidaBotella()
    {
        while (Vector3.Distance(botellaGas.transform.position, baseBotella.position) > 3f)
        {
            yield return null;
        }
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
        mascaraGas.SetActive(true);
        puertaBloqueo.SetActive(true);

        Mensaje("Riesgo actualizado, se ha encontrado riesgo de intoxicacion, abandona la sala inmediatamente");

        yield return EsperarLlegada(puertaBloqueo.transform);

        sonidoTokTok.Play();
        yield return new WaitForSeconds(sonidoTokTok.clip.length);

        Mensaje("Rapido, busca una mascara de gas, peligro de muerte inminente!");

        yield return new WaitForSeconds(4f);

        Mensaje("Mascara de gas en el techo inaccesible, busca una salida alternativa.");

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

        Mensaje("Perfecto, mascara de gas colocada, busca una salida para terminar el riesgo de intoxicacion");

        yield return EsperarLlegada(baseClon2);

        sonidoOUT.Play();
        puertaBloqueo.SetActive(false);
        puertaDimensionalNormal.SetActive(true);
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
        Mensaje("Alguna vez has tenido un sueno que parecia tan real que no podias distinguirlo de la realidad?");

        yield return EsperarLlegada(baseMatrixMIB);

        sonidoBang.Play();
        panelHUD.SetActive(false);
        pantallaFinal.SetActive(true);
        textoFinal.text = "Simulación Finalizada. TFM Máster en Industria 4.0 - Realizado por Moisés Sevilla Corrales - Curso 2025-2026.";
    }

    [Header("Fase 2 - Referencias")]
    public Transform jugador;
    public GameObject gasDimensional;
    public AudioSource sonidoTokTok;
    public AudioSource sonidoIN;
    public AudioSource sonidoOUT;
    public Transform baseNormal;
    public Transform baseCloneInicio;
    public Transform baseClon1;
    public Transform baseInicioDestino;
    public Transform baseClon2;
    public GameObject puertaDimensionalNormal;
    public Transform baseMatrixInicio;
    public Transform baseMatrixSillon;
    public Transform baseMatrixMIB;
    public AudioSource sonidoCaida;
    public AudioSource sonidoMatrix;
    public AudioSource sonidoBang;
    public GameObject pantallaFinal;
    public TextMeshProUGUI textoFinal;
    public GameObject panelHUD;

    [Header("Teletransporte")]
    public Transform raizJugador;
    public Transform raizCamara;

    private const float radioZona = 1.5f;

    IEnumerator EsperarLlegada(Transform destino)
    {
        while (Vector3.Distance(jugador.position, destino.position) > radioZona)
        {
            yield return null;
        }
    }

    void Teletransportar(Transform destino)
    {
        locomotor.enabled = false;
        raizJugador.position = destino.position;
        locomotor.enabled = true;
    }
}