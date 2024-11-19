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
        // Seguir al l�der o dirigirse a una salida
        //Debug.Log($"{gameObject.name} est� siguiendo a un l�der o saliendo del edificio...");
        // Agrega aqu� l�gica espec�fica para el movimiento del seguidor
        //searchTarget("Exit", 2f)
        searchTarget("Leader", 0f);
        // Moverse de forma aleatoria si no se encuentra el objeto "Leader" o hay obst�culos
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            SetRandomDestination();
        }
    }
}
