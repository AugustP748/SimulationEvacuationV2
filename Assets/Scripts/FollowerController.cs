using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowerController : AgentController
{

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void PerformBehavior()
    {
        // Seguir al líder o dirigirse a una salida
        //Debug.Log($"{gameObject.name} está siguiendo a un líder o saliendo del edificio...");
        // Agrega aquí lógica específica para el movimiento del seguidor
        //searchTarget("Exit", 2f)
        searchTarget("Leader", 0f);
        // Moverse de forma aleatoria si no se encuentra el objeto "Leader" o hay obstáculos
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            SetRandomDestination();
        }
    }
}
