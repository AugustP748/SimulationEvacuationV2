using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LeaderController : AgentController
{
    protected GameObject exitObject;
    private NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        exitObject = GameObject.FindGameObjectWithTag("Exit");
        if (exitObject != null)
        {
            setExitDestination();
        }
        else
        {
            Debug.LogError("No se encontró un objeto con la etiqueta 'Exit'");
        }
    }
    protected override void Update()
    {
        base.Update();
    }

    void setExitDestination()
    {
        Vector3 exitPosition = exitObject.transform.position;
        navMeshAgent.SetDestination(exitPosition);
    }


    
}
