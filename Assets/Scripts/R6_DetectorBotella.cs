using UnityEngine;

public class R6_DetectorBotella : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "R6_Botella_Gas")
        {
            SecuenciaR6.Instancia.ProcesarEvento("BotellaTocaBase");
        }
    }
}