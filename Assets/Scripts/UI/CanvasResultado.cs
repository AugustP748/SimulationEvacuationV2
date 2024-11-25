using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasResultado : MonoBehaviour
{
    [SerializeField] private GameObject panelResultados;
    [SerializeField] private TextMeshProUGUI textoMuertes;
    [SerializeField] private TextMeshProUGUI textoSalvados;
    [SerializeField] private TextMeshProUGUI textoTiempo;
    // Start is called before the first frame update
    void Start()
    {
        panelResultados.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MostrarResultados()
    {
        panelResultados.SetActive(true);

        textoMuertes.text = $"Cantidad de muertes: {GameManager.Instance.Muertes}";
        textoSalvados.text = $"Cantidad de salvados: {GameManager.Instance.Salvados}";
        textoTiempo.text = $"Tiempo de simulación: {GameManager.Instance.TiempoSimulacion:F2}s";

        Time.timeScale = 0f; // Pause the simulations
        AudioListener.pause = true;
    }
}

