using UnityEngine;

public class ZonaEntregaRiesgo : MonoBehaviour
{
    public string riesgoId;

    void OnTriggerEnter(Collider other)
    {
        RiesgoObjeto obj = other.GetComponent<RiesgoObjeto>();
        if (obj != null && obj.riesgoId == riesgoId && !obj.entregado)
        {
            obj.entregado = true;
            SecuenciaRiesgos.Instancia.ResolverRiesgo(riesgoId);
        }
    }
}