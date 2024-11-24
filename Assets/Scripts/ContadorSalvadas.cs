using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContadorSalvadas : MonoBehaviour
{
    private float salvadas = 0;
    public TextMeshProUGUI textMesh;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.text = "Gente salvada: " + salvadas;
    }

    public void IncrementarSalvadas()
    {
        salvadas++;
        textMesh.text = "Gente salvada: " + salvadas;
    }
}
