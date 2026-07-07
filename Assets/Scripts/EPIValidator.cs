using UnityEngine;
using UnityEngine.Events;

public enum EPIStatus { Pending, Correct, Incorrect }

public class EPIValidator : MonoBehaviour
{
    [Header("Config")]
    public string epiTag;                     //Tag del EPI esperado, ej. Casco
    public EPIStatus status = EPIStatus.Pending;

    [Header("Eventos Unity")]
    public UnityEvent OnEPIValidated;         //Aquí engancharás sonidocolor verde
    public UnityEvent OnEPIError;             //Aquí engancharás sonidocolor rojo

    private static readonly string[] tagsEPI = { "Casco", "Guantes", "Botas", "Gafas", "Earmuff" };

    void OnTriggerEnter(Collider other)
    {
        bool esEPI = System.Array.IndexOf(tagsEPI, other.tag) >= 0;
        if (!esEPI) return; // Ignora todo lo que no sea un EPI (manos, controlador, paredes...)

        if (other.CompareTag(epiTag))
        {
            status = EPIStatus.Correct;
            OnEPIValidated.Invoke();
        }
        else
        {
            status = EPIStatus.Incorrect;
            OnEPIError.Invoke();
        }
    }
}