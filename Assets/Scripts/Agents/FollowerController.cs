using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowerController : AgentController
{
    public float detectionRadius = 10f; // Rango en el que puede detectar líderes
    public LayerMask leaderLayer;       // Capa para identificar líderes
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void PerformBehavior()
    {

        if (CheckForExit())
        {
            return; // Si ve una salida, ya no busca líderes ni se mueve aleatoriamente
        }

        if (FollowLeader())
        {
            return; // Si encuentra un líder, lo sigue y no se mueve aleatoriamente
        }
        // Si no hay líder, se mueve de forma aleatoria
        MoveToRandomDestination();
    }

    private bool FollowLeader()
    {
        Collider[] leaders = Physics.OverlapSphere(transform.position, detectionRadius, leaderLayer);
        if (leaders.Length > 0)
        {
            // Si encuentra un líder, se dirige hacia él
            navMeshAgent.SetDestination(leaders[0].transform.position);
            //Debug.Log($"{gameObject.name} está siguiendo a {leaders[0].gameObject.name}");
            return true;
        }
        return false;
    }

    

}
