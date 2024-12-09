using System.Collections;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public float expansionRate = 2f;
    private float timer = 0f;
    private float expansionInterval = 3f;


    public int damageAmount = 25; // Cantidad de vida que quita el fuego
    public float damageInterval = 2f; // Intervalo en segundos entre cada daño
    private bool isPlayerInFire = false; // Para controlar si el jugador está en el fuego
    private Coroutine damageCoroutine;

    // Start is called before the first frame update
    void Start()
    {

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
        Vector3 newScale = transform.localScale + new Vector3(increment, increment, 1);
        if (newScale.y > 2)
        {
            newScale.y = 2;
        }
        transform.localScale = newScale;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Agent"))
        {
            AgentController agentController = other.gameObject.GetComponent<AgentController>();
            if (agentController != null)
            {
                isPlayerInFire = true;
                if (damageCoroutine == null) // Evitar múltiples corrutinas
                {
                    damageCoroutine = StartCoroutine(ApplyDamageOverTime(other.gameObject));
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Agent"))
        {
            
            isPlayerInFire = false;
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
            
        }
    }

    private IEnumerator ApplyDamageOverTime(GameObject player)
    {
        while (isPlayerInFire)
        {
            if (player != null && player.gameObject != null)
            {
                AgentController agentController = player.gameObject.GetComponent<AgentController>();
                if (agentController != null && agentController.health > 0)
                {
                    agentController.TakeDamage(damageAmount);
                    Debug.Log("colisiona " + player.name + agentController.health);
                }
            }
            yield return new WaitForSeconds(damageInterval);
        }
    }
}