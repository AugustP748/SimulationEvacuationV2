using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{
    [SerializeField] private ContadorMuertes muertes;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Agent"))
        {
            AgentController agentController = other.gameObject.GetComponent<AgentController>();
            if (agentController != null)
            {
                muertes.IncrementarMuertes();
                GameManager.Instance.RegistrarMuerte();
                Destroy(other.gameObject);
            }
        }
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject);
        }
    }

}
