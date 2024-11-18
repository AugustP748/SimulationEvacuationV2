using UnityEngine.AI;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    private Transform target; // Renamed to avoid serialization conflict
    public float wanderRadius = 10f;
    public float health = 1000f;


    // Reference to the Agents object

    // Start is called before the first frame update
    void Start()
    {
        //SetRandomDestination();

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        searchTarget("Fire", 20f);
    }

    protected void searchTarget(string targetTag, float value)
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        // Buscar el objeto con el tag "Leader"
        GameObject leaderObject = GameObject.FindGameObjectWithTag(targetTag);
        if (leaderObject != null)
        {
            target = leaderObject.transform;
            // Verificar si no hay obstáculos entre el agente y el objeto "Leader"
            if (HasLineOfSight(target))
            {
                if (targetTag == "Fire")
                {
                    // Aumentar la velocidad del agente
                    agent.speed = value;

                }
                else
                {
                    agent.SetDestination(target.position);
                    agent.stoppingDistance = value; // Corrected line
                }
                return;
            }
        }
    }

    protected bool HasLineOfSight(Transform target)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, (target.position - transform.position).normalized, out hit))
        {
            if (hit.transform == target)
            {
                return true;
            }
        }
        return false;
    }

    protected void SetRandomDestination()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
        agent.SetDestination(newPos);
    }

    private static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}