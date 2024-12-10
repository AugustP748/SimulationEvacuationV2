using System.Collections;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public float expansionRate = 2f;
    private float timer = 0f;
    private float expansionInterval = 3f;

    private float timeSinceLastDamage = 0f; // Temporizador para controlar el intervalo

    public int damageAmount = 25; // Cantidad de vida que quita el fuego
    public float damageInterval = 2f; // Intervalo en segundos entre cada daño
    //private bool isPlayerInFire = false; // Para controlar si el jugador está en el fuego
    private Coroutine damageCoroutine;

    public float explosionForce = 500f; // Fuerza de la explosión
    public float explosionRadius = 5f; // Radio de la explosión
    public float upwardsModifier = 1f; // Elevación de los objetos por la explosión

    // Start is called before the first frame update
    void Start()
    {
        // Posición de la explosión
        Vector3 explosionPosition = transform.position;

        // Llama a la explosión
        TriggerExplosion(explosionPosition);

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= expansionInterval)
        {
            ExpandObject(expansionRate);
            timer = 0f;
        }
    }

    void ExpandObject(float increment)
    {
        Vector3 newScale = transform.localScale + new Vector3(increment, increment, increment);
        if (newScale.y > 2)
        {
            newScale.y = 2;
        }
        transform.localScale = newScale;
    }

    public void TriggerExplosion(Vector3 explosionPosition)
    {
        // Encuentra todos los colliders con el tag "obstacle" en el rango de la explosión
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius, LayerMask.GetMask("obstacle"));

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Aplica la fuerza explosiva
                rb.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, upwardsModifier);
            }
        }

    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.layer == LayerMask.NameToLayer("Agent"))
    //    {
    //        AgentController agentController = other.gameObject.GetComponent<AgentController>();
    //        if (agentController != null)
    //        {
    //            agentController.TakeDamage(25f);
    //            Debug.Log("colisiona " + agentController.health);
    //        }
    //    }
    //}


    //chat alternativa 1
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.layer == LayerMask.NameToLayer("Agent"))
    //    {
    //        AgentController agentController = other.gameObject.GetComponent<AgentController>();
    //        if (agentController != null)
    //        {
    //            isPlayerInFire = true;
    //            if (damageCoroutine == null) // Evitar múltiples corrutinas
    //            {
    //                damageCoroutine = StartCoroutine(ApplyDamageOverTime(other.gameObject));
    //            }
    //        }
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.layer == LayerMask.NameToLayer("Agent"))
    //    {

    //        isPlayerInFire = false;
    //        if (damageCoroutine != null)
    //        {
    //            StopCoroutine(damageCoroutine);
    //            damageCoroutine = null;
    //        }

    //    }
    //}

    //private IEnumerator ApplyDamageOverTime(GameObject player)
    //{
    //    while (isPlayerInFire)
    //    {
    //        if (player != null && player.gameObject != null)
    //        {
    //            AgentController agentController = player.gameObject.GetComponent<AgentController>();
    //            if (agentController != null && agentController.health > 0)
    //            {
    //                agentController.TakeDamage(damageAmount);
    //                Debug.Log("colisiona " + player.name + agentController.health);
    //            }
    //        }
    //        yield return new WaitForSeconds(damageInterval);
    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Agent")) // Verifica si el jugador está en el fuego
        {
            timeSinceLastDamage += Time.deltaTime; // Incrementa el temporizador

            if (timeSinceLastDamage >= damageInterval) // Aplica daño si se supera el intervalo
            {
                AgentController agentController = other.gameObject.GetComponent<AgentController>();
                if (agentController != null)
                {
                    agentController.TakeDamage(damageAmount);
                    //Debug.Log("Daño aplicado en "+ agentController.name + ": " + agentController.health );
                }

                timeSinceLastDamage = 0f; // Reinicia el temporizador
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Agent"))
        {
            timeSinceLastDamage = 0f; // Reinicia el temporizador al salir del fuego
        }
    }
}