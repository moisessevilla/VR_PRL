using System.Collections;
using UnityEngine;

public class EPIItem : MonoBehaviour
{
    public string epiId;

    private Vector3 posicionInicial;
    private bool tocado = false;
    private bool listo = false;
    private const float umbral = 0.15f;
    private const float tiempoRearme = 1.5f;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        posicionInicial = transform.position;
        listo = true;
    }

    void Update()
    {
        if (!listo || tocado) return;

        float distancia = Vector3.Distance(transform.position, posicionInicial);

        if (distancia > umbral)
        {
            tocado = true;
            SecuenciaEPIs.Instancia.IntentarColocar(epiId, gameObject);
            StartCoroutine(Rearmar());
        }
    }

    IEnumerator Rearmar()
    {
        yield return new WaitForSeconds(tiempoRearme);
        posicionInicial = transform.position; // referencia nueva, donde haya quedado
        tocado = false;
    }
}