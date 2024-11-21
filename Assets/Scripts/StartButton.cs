using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public GameObject evacuationSimulation;

    [SerializeField] private TiempoSimulacion tiempoSimulacion;
    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    public void SpawnFire()
    {
        evacuationSimulation.GetComponent<EvacuationSImulation>().StartSimulation();
        tiempoSimulacion.StartSimulation();
    }
}
