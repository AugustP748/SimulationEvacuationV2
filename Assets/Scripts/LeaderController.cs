using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LeaderController : AgentController
{
    protected GameObject exitObject;
    //private NavMeshAgent navMeshAgent;

    protected override void Update()
    {
        base.Update();
    }

    public override void PerformBehavior()
    {

        //Seguir al líder o dirigirse a una salida
        //Debug.Log($"{gameObject.name} está liderando o saliendo del edificio...");
        //Agrega aquí lógica específica para el movimiento del seguidor

       exitObject = GameObject.FindGameObjectWithTag("Exit");
        if (exitObject != null)
        {
            setExitDestination();
        }
        else
        {
            Debug.LogError("No se encontró un objeto con la etiqueta 'exit'");
        }
    }

    void setExitDestination()
    {
        Vector3 exitPosition = exitObject.transform.position;
        navMeshAgent.SetDestination(exitPosition);
    }
    
}
