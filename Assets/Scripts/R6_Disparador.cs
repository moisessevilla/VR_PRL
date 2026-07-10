using UnityEngine;

public class R6_Disparador : MonoBehaviour
{
    public string evento;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        SecuenciaR6.Instancia.ProcesarEvento(evento);
    }
}