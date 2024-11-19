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
        // Seguir al l�der o dirigirse a una salida
        //Debug.Log($"{gameObject.name} est� explorando buscando una salida...");
        // Agrega aqu� l�gica espec�fica para el movimiento del seguidor
        // Moverse de forma aleatoria si no se encuentra el objeto "Exit" o hay obst�culos
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            SetRandomDestination();
        }
        searchTarget("Exit", 2f);
    }

}

