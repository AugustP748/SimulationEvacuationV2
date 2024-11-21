using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TiempoSimulacion : MonoBehaviour
{
    private float tiempo = 0;
    public TextMeshProUGUI textMesh;
    private bool simulacionIniciada = false;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.text = "Tiempo de simulación: 0s";
    }

    private void Update()
    {
        if (simulacionIniciada)
        {
            tiempo += Time.deltaTime;
            textMesh.text = "Tiempo de simulación: " + tiempo.ToString("F2") + "s";
        }
    }

    public void StartSimulation()
    {
        simulacionIniciada = true;
    }
}
