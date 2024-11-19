using UnityEngine.AI;
using UnityEngine;
using System.Collections;

public abstract class AgentController : MonoBehaviour
{
    private Transform target;
    protected NavMeshAgent navMeshAgent;
    public float minWanderRadius = 5f;
    public float maxWanderRadius = 10f;
    protected float wanderRadius;
    public float health = 100f;
    protected bool hasHeardAlarm = false;
    public float hearingRadius = 20f;
    public float fieldOfViewAngle = 110f; // Campo de visión (en grados)
    // Reference to the Agents object

    // Start is called before the first frame update
    protected virtual void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        wanderRadius = Random.Range(minWanderRadius, maxWanderRadius);
        SetRandomDestination();

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!hasHeardAlarm)
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending)
            {
                SetRandomDestination(); // Generar un nuevo destino cuando llega al anterior
            }
            CheckForAlarm(); // Detectar si hay una alarma sonando
            CheckForVision(); // Verificación de visión (si aplica)
        }
        else
        {
                PerformBehavior(); // Comportamiento específico según el tipo de agente

        }
    }

    // Detectar una alarma dentro del rango
    protected void CheckForAlarm()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, hearingRadius);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Alarm")) // Suponiendo que la alarma tiene esta etiqueta
            {
                AudioSource alarmAudio = collider.GetComponent<AudioSource>();
                if (alarmAudio != null && alarmAudio.isPlaying)
                {
                    //Debug.Log($"{gameObject.name} ha detectado la alarma.");
                    hasHeardAlarm = true;
                    break;
                }
            }
        }
    }

    protected void CheckForVision()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, hearingRadius);
        foreach (var target in targetsInViewRadius)
        {
            Vector3 directionToTarget = (target.transform.position - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, directionToTarget);

            if (angle < fieldOfViewAngle / 2)
            {
                //Debug.Log($"{gameObject.name} ve {target.name}");
                // Aquí puedes agregar lógica adicional, como reaccionar al objeto visto
                if (target.tag == "Fire")
                {
                    //Debug.Log($"{gameObject.name} ve el fuego");
                    hasHeardAlarm = true;
                    break;
                }

            }
        }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Exit"))
        {
            Destroy(gameObject);
        }
    }

    

    public abstract void PerformBehavior(); // Método abstracto para sobrescribir en las clases hijas
}
