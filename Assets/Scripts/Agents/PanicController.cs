using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PanicController : AgentController
{
    //private NavMeshAgent navMeshAgent;
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
                                  // Seguir al líder o dirigirse a una salida
                                  //Debug.Log($"{gameObject.name} está histérico...");
                                  // Agrega aquí lógica específica para el movimiento del seguidor
        //MoveToRandomDestination();


        //if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        //{
        //    SetRandomDestination();
        //}
        //searchTarget("Exit", 2f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!behaviorPerformed) return; // Only execute if PerformBehavior has been called

        if (other.gameObject.CompareTag("Follower") || other.gameObject.CompareTag("Explorer") ||
            other.gameObject.CompareTag("Leader") || other.gameObject.CompareTag("PanicStriker"))
        {
            AgentController otherAgent = other.gameObject.GetComponent<AgentController>();
            ApplyKnockback(otherAgent, other.contacts[0].point);
            Debug.Log($"{gameObject.name} hizo caer a {other.gameObject.name}");
        }
    }

    private void ApplyKnockback(AgentController otherAgent, Vector3 collisionPoint)
    {
        // Agregar fuerza al agente afectado
        Rigidbody rb = otherAgent.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 knockbackDirection = (otherAgent.transform.position - collisionPoint).normalized;
            rb.AddForce(knockbackDirection * 10f, ForceMode.Impulse);

            // Opcional: Desactivar temporalmente el NavMeshAgent del agente afectado para simular "caída"
            NavMeshAgent agent = otherAgent.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.enabled = false;
                StartCoroutine(ReenableNavMeshAgent(agent, 3f)); // Reactivar después de 2 segundos
            }
        }
    }

    private System.Collections.IEnumerator ReenableNavMeshAgent(NavMeshAgent agent, float delay)
    {
        yield return new WaitForSeconds(delay);
        agent.enabled = true;
    }
}



