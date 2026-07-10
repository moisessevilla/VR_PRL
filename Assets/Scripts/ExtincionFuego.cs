using UnityEngine;
using System.Collections;

public class ExtincionFuego : MonoBehaviour
{
    public string riesgoId;
    public AudioSource sonidoExtintor;
    public GameObject fuego;
    public GameObject puerta;

    public GameObject baseObjeto;
    private bool activado = false;

    void OnTriggerEnter(Collider other)
    {
        if (activado) return;

        RiesgoObjeto obj = other.GetComponent<RiesgoObjeto>();
        if (obj != null && obj.riesgoId == riesgoId)
        {
            activado = true;
            StartCoroutine(ApagarFuego());
        }
    }

    IEnumerator ApagarFuego()
    {
        sonidoExtintor.Play();
        yield return new WaitForSeconds(sonidoExtintor.clip.length);

        fuego.SetActive(false);
        puerta.SetActive(false);
        baseObjeto.SetActive(false);
        sonidoExtintor.Stop();

        SecuenciaRiesgos.Instancia.ResolverRiesgo(riesgoId);
    }
}