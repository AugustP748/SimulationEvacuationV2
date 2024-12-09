using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TiempoSimulacion : MonoBehaviour
{
    private float tiempo = 0;
    public TextMeshProUGUI textMesh;
    public float tiempoSimulacion = 5f;
    private bool simulacionIniciada = false;
    [SerializeField] private GameObject panelUI;
    [SerializeField] private CanvasResultado canvasResultado; // Arrastra el objeto aquí desde el inspector


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

            //if (tiempo >= tiempoSimulacion)
            //{
            //    StopSimulation();
            //}
        }
    }

    public void StartSimulation()
    {
        simulacionIniciada = true;
    }

    public void StopSimulation()
    {

        simulacionIniciada = false;
        panelUI.SetActive(false);
        GameManager.Instance.RegistrarTiempo(tiempo);

        // Muestra resultados
        canvasResultado.MostrarResultados();

    }

}
