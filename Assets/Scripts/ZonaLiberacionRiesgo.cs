using UnityEngine;

public class ZonaLiberacionRiesgo : MonoBehaviour
{
    public string riesgoId;
    public Transform objetoAVigilar;
    public float radioZona = 0.5f;
    private bool resuelto = false;

    void Update()
    {
        if (resuelto) return;

        float distancia = Vector3.Distance(transform.position, objetoAVigilar.position);

        if (distancia > radioZona)
        {
            resuelto = true;
            SecuenciaRiesgos.Instancia.ResolverRiesgo(riesgoId);
        }
    }
}