using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PanicController : AgentController
{
    public float pushForce = 5f; // Fuerza de empuje
    private bool behaviorPerformed = false; // Flag to check if PerformBehavior has been executed

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void PerformBehavior()
    {
        behaviorPerformed = true; // Set the flag to true when PerformBehavior is executed
        MoveToRandomDestination();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasHeardAlarm)
        {
            if (collision.gameObject.CompareTag("Agent"))
            {
                Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 pushDirection = collision.transform.position - transform.position;
                    pushDirection.y = 0; // Ensure the push is horizontal
                    rb.AddForce(pushDirection.normalized * pushForce, ForceMode.Impulse);
                    StartCoroutine(HandleAgentFall(collision.gameObject));
                }
            }
        }
        
    }

    private IEnumerator HandleAgentFall(GameObject agent)
    {
        NavMeshAgent agentNavMesh = agent.GetComponent<NavMeshAgent>();
        if (agentNavMesh != null)
        {
            if (agentNavMesh.gameObject.activeInHierarchy) // Check if the object is active in the hierarchy
            {
                agentNavMesh.enabled = false; // Disable NavMeshAgent to simulate fall
                yield return new WaitForSeconds(3f); // Wait for 3 seconds
                if (agentNavMesh != null) // Check if the object is still not null
                {
                    agentNavMesh.enabled = true; // Re-enable NavMeshAgent to continue activity
                }
            }
        }
    }
}



