using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowerController : AgentController
{
    public float detectionRadius = 10f; // Rango en el que puede detectar l�deres
    public LayerMask leaderLayer;       // Capa para identificar l�deres
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void PerformBehavior()
    {

        if (CheckForExit())
        {
            return; // Si ve una salida, ya no busca l�deres ni se mueve aleatoriamente
        }

        if (FollowLeader())
        {
            return; // Si encuentra un l�der, lo sigue y no se mueve aleatoriamente
        }
        // Si no hay l�der, se mueve de forma aleatoria
        MoveToRandomDestination();
    }

    private bool FollowLeader()
    {
        Collider[] leaders = Physics.OverlapSphere(transform.position, detectionRadius, leaderLayer);
        if (leaders.Length > 0)
        {
            // Si encuentra un l�der, se dirige hacia �l
            navMeshAgent.SetDestination(leaders[0].transform.position);
            //Debug.Log($"{gameObject.name} est� siguiendo a {leaders[0].gameObject.name}");
            return true;
        }
        return false;
    }

    

}
