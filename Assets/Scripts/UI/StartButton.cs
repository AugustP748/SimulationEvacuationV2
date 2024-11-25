using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public GameObject evacuationSimulation;

    [SerializeField] private TiempoSimulacion tiempoSimulacion;

    public void SpawnFire()
    {
        evacuationSimulation.GetComponent<EvacuationSImulation>().StartSimulation();
        tiempoSimulacion.StartSimulation();
        Destroy(gameObject);
    }
}
