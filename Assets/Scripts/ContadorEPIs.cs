using UnityEngine;
using TMPro;

public class ContadorEPIs : MonoBehaviour
{
    public TextMeshProUGUI texto;
    private int correctos = 0;
    private int totalEPIs = 4;

    public void SumarCorrecto()
    {
        correctos++;
        texto.text = "EPIs correctos: " + correctos + "/" + totalEPIs;
    }
}