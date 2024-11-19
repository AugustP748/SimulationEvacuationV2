using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ExplorerAgent : AgentController
{

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void PerformBehavior()
    {
        // Seguir al líder o dirigirse a una salida
        //Debug.Log($"{gameObject.name} está explorando buscando una salida...");
        // Agrega aquí lógica específica para el movimiento del seguidor
        // Moverse de forma aleatoria si no se encuentra el objeto "Exit" o hay obstáculos
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            SetRandomDestination();
        }
        searchTarget("Exit", 2f);
    }

}

