using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AlarmControl : MonoBehaviour
{
    private Transform firetarget;
    public Color fireDetectedColor = Color.red;
    private Renderer objectRenderer;

    // Start is called before the first frame update
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFireNearby())
        {
            ChangeColor(fireDetectedColor);
        }
    }

    private bool IsFireNearby()
    {
        GameObject leaderObject = GameObject.FindGameObjectWithTag("Fire");
        if (leaderObject != null)
        {
            firetarget = leaderObject.transform;
        }
        RaycastHit hit;
        if (firetarget != null && Physics.Raycast(transform.position, (firetarget.position - transform.position).normalized, out hit))
        {
            if (hit.transform == firetarget)
            {
                return true;
            }
        }
        return false;
    }

    private void ChangeColor(Color newColor)
    {
        if (objectRenderer != null)
        {
            objectRenderer.material.color = newColor;
        }
    }
}
