using UnityEngine.AI;
using UnityEngine;
using System.Collections;

public abstract class AgentController : MonoBehaviour
{
    private Transform target;
    protected NavMeshAgent navMeshAgent;
    public float minWanderRadius = 4f;
    public float maxWanderRadius = 7f;
    protected float wanderRadius;
    public float health = 100f;
    protected bool hasHeardAlarm = false;
    public float hearingRadius = 20f;
    public float fieldOfViewAngle = 110f; // Campo de visi�n (en grados)
    public LayerMask exitLayer;
    private bool isSearching = true;      // Controla si el agente est� buscando un destino

    [SerializeField] private ContadorSalvadas salvadas;
    [SerializeField] private ContadorMuertes muertes;


    // Reference to the Agents object

    // Start is called before the first frame update
    protected virtual void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        wanderRadius = Random.Range(minWanderRadius, maxWanderRadius);
        //SetRandomDestination();
        //navMeshAgent.speed = 20f;

    }

    protected virtual void Update()
    {
        if (!hasHeardAlarm)
        {
            CheckForAlarm();
            CheckForVision();
            HandleWandering(); // A�adimos el comportamiento de b�squeda y movimiento
        }
        else
        {
            PerformBehavior();
        }
    }


    private void HandleWandering()
    {
        if (isSearching)
        {
            SetRandomDestination(); // O encuentra otro punto aleatorio
        }
        else
        {
            // Verificar si lleg� al destino
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                isSearching = true; // Reanuda el raycasting
                SetRandomDestination(); // O encuentra otro punto aleatorio
            }
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
                // Aqu� puedes agregar l�gica adicional, como reaccionar al objeto visto
                if (target.CompareTag("Fire"))
                {
                    //Debug.Log($"{gameObject.name} ve el fuego");
                    hasHeardAlarm = true;
                    break;
                }

            }
        }
    }

    protected bool CheckForExit()
    {
        Collider[] exits = Physics.OverlapSphere(transform.position, hearingRadius, exitLayer);
        if (exits.Length > 0)
        {
            // Si encuentra una salida, se dirige a ella
            navMeshAgent.SetDestination(exits[0].transform.position);
            Debug.Log($"{gameObject.name} detect� una salida y se dirige hacia ella.");
            return true;
        }
        return false;
    }

 

    protected void MoveToRandomDestination()
    {
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending)
        {
            SetRandomDestination(); // Generar un nuevo destino cuando llega al anterior
        }
    }

    protected void SetRandomDestination()
    {
        // Genera un punto aleatorio en un rango
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position; // Translada el punto aleatorio al �rea del agente

        // Asegura que el punto est� en el NavMesh
        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, wanderRadius, NavMesh.AllAreas))
        {
            navMeshAgent.SetDestination(hit.position);
        }
        //NavMeshAgent agent = GetComponent<NavMeshAgent>();
        //Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
        //agent.SetDestination(newPos);
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
            muertes.IncrementarMuertes();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Exit"))
        {
            salvadas.IncrementarSalvadas();
            Destroy(gameObject);
        }
    }

    public abstract void PerformBehavior(); // M�todo abstracto para sobrescribir en las clases hijas
}



//protected void searchTarget(string targetTag, float value)
//{
//    NavMeshAgent agent = GetComponent<NavMeshAgent>();
//    // Buscar el objeto con el tag "Leader"
//    GameObject leaderObject = GameObject.FindGameObjectWithTag(targetTag);
//    if (leaderObject != null)
//    {
//        target = leaderObject.transform;
//        // Verificar si no hay obst�culos entre el agente y el objeto "Leader"
//        if (HasLineOfSight(target))
//        {
//            if (targetTag == "Fire")
//            {
//                // Aumentar la velocidad del agente
//                agent.speed = value;

//            }
//            else
//            {
//                agent.SetDestination(target.position);
//                agent.stoppingDistance = value; // Corrected line
//            }
//            return;
//        }
//    }
//}

//protected bool HasLineOfSight(Transform target)
//{
//    RaycastHit hit;
//    if (Physics.Raycast(transform.position, (target.position - transform.position).normalized, out hit))
//    {
//        if (hit.transform == target)
//        {
//            return true;
//        }
//    }
//    return false;
//}