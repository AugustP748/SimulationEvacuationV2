using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PanicController : AgentController
{
    private NavMeshAgent navMeshAgent;
    public float pushForce = 5f; // Fuerza de empuje

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

        // Moverse de forma aleatoria si no se encuentra el objeto "Exit" o hay obstáculos
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            SetRandomDestination();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Agent") || collision.gameObject.CompareTag("Agent"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 pushDirection = collision.transform.position - transform.position;
                pushDirection.y = 0; // No empujar hacia arriba o abajo
                rb.AddForce(pushDirection.normalized * pushForce, ForceMode.Impulse);

                // Aplicar fuerza hacia abajo para tirar al piso
                rb.AddForce(Vector3.down * pushForce, ForceMode.Impulse);
            }
        }
    }
}
