using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PanicController : AgentController
{
    //private NavMeshAgent navMeshAgent;
    public float pushForce = 5f; // Fuerza de empuje

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void PerformBehavior()
    {
        // Seguir al líder o dirigirse a una salida
        //Debug.Log($"{gameObject.name} está histérico...");
        // Agrega aquí lógica específica para el movimiento del seguidor
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            SetRandomDestination();
        }
        searchTarget("Exit", 2f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Leader") || other.gameObject.CompareTag("PanicStriker") || other.gameObject.CompareTag("Explorer") || other.gameObject.CompareTag("Follower"))
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 pushDirection = other.transform.position - transform.position;
                pushDirection.y = 0; // No empujar hacia arriba o abajo
                rb.AddForce(pushDirection.normalized * pushForce, ForceMode.Impulse);

                // Aplicar fuerza hacia abajo para tirar al piso
                rb.AddForce(Vector3.down * pushForce, ForceMode.Impulse);
            }
        }
    }
}
