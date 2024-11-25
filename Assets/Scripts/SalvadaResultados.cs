using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SalvadaResultados : MonoBehaviour
{
    public TextMeshProUGUI contadorSalvadasText;

    private void Start()
    {
        contadorSalvadasText = GetComponent<TextMeshProUGUI>();
        contadorSalvadasText.text = "Cantidad de salvadas: ";

    }

    // Update is called once per frame
    void Update()
    {

    }
}
