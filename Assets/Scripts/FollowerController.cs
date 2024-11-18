using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowerController : AgentController
{

    private NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        SetRandomDestination();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        searchTarget("Exit", 2f);
        searchTarget("Leader", 0f);

        // Moverse de forma aleatoria si no se encuentra el objeto "Leader" o hay obstáculos
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            SetRandomDestination();
        }
    }
}
