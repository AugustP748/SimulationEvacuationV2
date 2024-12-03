using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int Muertes { get; private set; }
    public int Salvados { get; private set; }
    public float TiempoSimulacion { get; private set; }
    public int AgentCount { get; private set; } // New property for counting objects with tag "Agent"

    private bool isPaused = false;

    [SerializeField] private ContadorSalvadas salvadas;
    [SerializeField] private ContadorMuertes muertes;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            AudioListener.pause = true;
            Time.timeScale = 0f; // Pausa el tiempo
            // Muestra el menú de pausa
            //UIManager.Instance.ShowPauseMenu();
        }
        else
        {
            Time.timeScale = 1f; // Reanuda el tiempo
            AudioListener.pause = false;
            // Oculta el menú de pausa
            //UIManager.Instance.HidePauseMenu();
        }
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    public void RegistrarMuerte()
    {
        Muertes++;
        muertes.IncrementarMuertes();
    }

    public void RegistrarSalvado()
    {
        Salvados++;
        salvadas.IncrementarSalvadas();
    }

    public void RegistrarTiempo(float tiempo) => TiempoSimulacion = tiempo;

    public void ResetDatos()
    {
        Muertes = 0;
        Salvados = 0;
        TiempoSimulacion = 0;
    }

    private void CountAgents()
    {
        GameObject[] agents = GameObject.FindGameObjectsWithTag("Agent");
        AgentCount = agents.Length;
    }
}
