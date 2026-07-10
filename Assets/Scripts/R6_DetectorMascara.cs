using System.Collections;
using UnityEngine;

public class R6_DetectorMascara : MonoBehaviour
{
    private Vector3 posicionAnterior;
    private bool tocado = false;
    private bool listo = false;
    private const float umbralPorFrame = 0.01f; // Controla el movimiento de la máscara

    IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        posicionAnterior = transform.position;
        listo = true;
    }

    void Update()
    {
        if (!listo || tocado) return;

        float delta = Vector3.Distance(transform.position, posicionAnterior);

        if (delta > umbralPorFrame)
        {
            tocado = true;
            SecuenciaR6.Instancia.ProcesarEvento("MascaraCogida");
        }
        else
        {
            posicionAnterior = transform.position;
        }
    }
}