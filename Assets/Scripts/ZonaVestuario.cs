using UnityEngine;

public class ZonaVestuario : MonoBehaviour
{
    private bool iniciado = false;

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (!iniciado)
        {
            iniciado = true;
            SecuenciaRiesgos.Instancia.IniciarRecorrido();
        }
    }
}