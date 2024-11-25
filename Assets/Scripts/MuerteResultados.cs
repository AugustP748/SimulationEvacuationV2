using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MuerteResultados : MonoBehaviour
{
    public TextMeshProUGUI contadorMuertesText;
    //public TextMeshProUGUI contadorSalvadasText;
    //public TextMeshProUGUI tiempoSimulacionText;

    private void Start()
    {

        contadorMuertesText = GetComponent<TextMeshProUGUI>();
        contadorMuertesText.text = "Cantidad de muertes: " + 0;
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
