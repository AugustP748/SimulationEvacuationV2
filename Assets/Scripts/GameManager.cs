using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int Muertes { get; private set; }
    public int Salvados { get; private set; }
    public float TiempoSimulacion { get; private set; }
    public int AgentCount
    {
        get { return agentCount; }
        set { agentCount = value; }
    }
    private int agentCount;

    [SerializeField] private TiempoSimulacion tiempoSimulacionObject;
    [SerializeField] private GameObject Pausa;
    private bool isPaused = false;
    public bool simulationEnded = false;

    [SerializeField] private ContadorSalvadas salvadas;
    [SerializeField] private ContadorMuertes muertes;

    public void Start()
    {
        Pausa.SetActive(false);
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(!simulationEnded)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }
        
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            AudioListener.pause = true;
            Time.timeScale = 0f; // Pausa el tiempo
            Pausa.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f; // Reanuda el tiempo
            AudioListener.pause = false;
            Pausa.SetActive(false);
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
        CheckSimulationStatus();
    }

    public void RegistrarSalvado()
    {
        Salvados++;
        salvadas.IncrementarSalvadas();
        CheckSimulationStatus();
    }

    private void CheckSimulationStatus()
    {
        if (Muertes + Salvados == AgentCount)
        {
            simulationEnded = true;
            StopSimulation();
        }
    }

    private void StopSimulation()
    {
        tiempoSimulacionObject.StopSimulation();
    }

    public void RegistrarTiempo(float tiempo) => TiempoSimulacion = tiempo;

    public void ResetDatos()
    {
        Muertes = 0;
        Salvados = 0;
        TiempoSimulacion = 0;
        Pausa.SetActive(false);
    }




    //private void CountAgents()
    //{
    //    GameObject[] agents = GameObject.FindGameObjectsWithTag("Agent");
    //    AgentCount = agents.Length;
    //}
}
