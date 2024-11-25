using UnityEngine;

public class FireController : MonoBehaviour
{
    public float expansionRate = 2f;
    private float timer = 0f;
    private float expansionInterval = 3f;

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

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Leader") || other.gameObject.CompareTag("PanicStriker") || other.gameObject.CompareTag("Explorer") || other.gameObject.CompareTag("Follower"))
        {
            AgentController agentController = other.gameObject.GetComponent<AgentController>();
            if (agentController != null)
            {
                agentController.TakeDamage(25f);
                //Debug.Log("colisiona " + agentController.health);
            }
        }
    }
}