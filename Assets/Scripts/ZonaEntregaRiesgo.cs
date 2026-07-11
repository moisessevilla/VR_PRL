using UnityEngine;

public class ZonaEntregaRiesgo : MonoBehaviour
{
    public string riesgoId;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ha entrado: " + other.name);

        RiesgoObjeto obj = other.GetComponent<RiesgoObjeto>();

        if (obj != null)
        {
            Debug.Log("Riesgo: " + obj.riesgoId);
            Debug.Log("Entregado: " + obj.entregado);
        }

        if (obj != null && obj.riesgoId == riesgoId && !obj.entregado)
        {
            obj.entregado = true;
            Debug.Log("RIESGO RESUELTO");
            SecuenciaRiesgos.Instancia.ResolverRiesgo(riesgoId);
        }
    }
}