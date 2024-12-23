using System.Collections;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public float expansionRate = 2f;
    private float timer = 0f;
    private float expansionInterval = 3f;

    private float timeSinceLastDamage = 0f; // Temporizador para controlar el intervalo

    public int damageAmount = 25; // Cantidad de vida que quita el fuego
    public float damageInterval = 2f; // Intervalo en segundos entre cada da�o
    //private bool isPlayerInFire = false; // Para controlar si el jugador est� en el fuego
    private Coroutine damageCoroutine;

    public float explosionForce = 500f; // Fuerza de la explosi�n
    public float explosionRadius = 5f; // Radio de la explosi�n
    public float upwardsModifier = 1f; // Elevaci�n de los objetos por la explosi�n

    // Start is called before the first frame update
    void Start()
    {
        // Posici�n de la explosi�n
        Vector3 explosionPosition = transform.position;

        // Llama a la explosi�n
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
        // Encuentra todos los colliders con el tag "obstacle" en el rango de la explosi�n
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
    //            if (damageCoroutine == null) // Evitar m�ltiples corrutinas
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
        if (other.gameObject.layer == LayerMask.NameToLayer("Agent")) // Verifica si el jugador est� en el fuego
        {
            timeSinceLastDamage += Time.deltaTime; // Incrementa el temporizador

            if (timeSinceLastDamage >= damageInterval) // Aplica da�o si se supera el intervalo
            {
                AgentController agentController = other.gameObject.GetComponent<AgentController>();
                if (agentController != null)
                {
                    agentController.TakeDamage(damageAmount);
                    //Debug.Log("Da�o aplicado en "+ agentController.name + ": " + agentController.health );
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