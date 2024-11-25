using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultadoTiempo : MonoBehaviour
{
    public TextMeshProUGUI contadorTiempoText;

    private void Start()
    {
        contadorTiempoText = GetComponent<TextMeshProUGUI>();
        contadorTiempoText.text = "Tiempo de simulacion: ";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
