using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LeaderController : AgentController
{
    protected GameObject exitObject;
    protected GameObject[] exitSignObjects;
    protected int currentExitIndex = 0;
    public float agentSpeed = 20f; // Added agent speed variable

    
    private float rayLengthLeader = 120f; // Longitud del Raycast
    private int numberOfRays = 10;  // Número de rayos en abanico
    private float spreadAngle = 20f; // Ángulo del abanico en grados
    private float upwardAngle = 190f; // Ángulo hacia arriba en grados
    //private float rayHeightOffsetLeader = 4.17f; // Altura desde la que se lanzan los rayos
    //public LayerMask exitLayer; // Capa para identificar los carteles "Exit"

    protected override void Update()
    {
        base.Update();
    }

    public override void PerformBehavior()
    {


        exitObject = GameObject.FindGameObjectWithTag("Exit");
        if (exitObject != null)
        {
            setExitDestination();
            Perform360Raycast();
            //Transform targetExit = FindExitUsingRaycast();
        }
        else
        {
            Debug.LogError("No se encontro un objeto con la etiqueta 'exit'");
        }
    }


    void setExitDestination()
    {
        Vector3 exitPosition = exitObject.transform.position;
        navMeshAgent.SetDestination(exitPosition);
    }

    public void Perform360Raycast()
    {
        float minDistance = Mathf.Infinity;
        int numberOfRays = 36;
        float angleIncrement = 360f / numberOfRays;
        float upwardAngle = 110f;
        float raycastDistance = 30f;

        for (int i = 0; i < numberOfRays; i++)
        {
            float angle = i * angleIncrement;
            Quaternion rotation = Quaternion.Euler(upwardAngle, angle, 0f);
            Vector3 direction = rotation * (transform.forward + Vector3.up * 1.14f) * -1;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, rayLengthLeader))
            {
                // Verificar si es un cartel (usando tanto layer como tag)
                bool isExitSign = ((1 << hit.collider.gameObject.layer) & exitLayer) != 0
                                 && hit.collider.CompareTag("ExitSignal");

                if (isExitSign)
                {
                    float distance = hit.distance;
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        Debug.DrawRay(transform.position, direction * hit.distance, Color.green);
                    }
                }
            }

        }
    }






    Transform FindExitUsingRaycast()
    {
        Transform closestExit = null;
        float minDistance = Mathf.Infinity;

        // Comenzar el raycast desde la posición del agente
        Vector3 rayStart = transform.position;

        for (int i = 0; i < numberOfRays; i++)
        {
            // Calcular el ángulo horizontal para el abanico
            float horizontalAngle = -spreadAngle / 2 + (spreadAngle / (numberOfRays - 1)) * i;

            // Calcular la dirección del rayo considerando tanto el ángulo horizontal como vertical
            // Primero rotamos horizontalmente
            Quaternion horizontalRotation = Quaternion.Euler(0, horizontalAngle, 0);
            Vector3 horizontalDirection = horizontalRotation * transform.forward;

            // Realizar el raycast
            RaycastHit hit;
            if (Physics.Raycast(rayStart, horizontalDirection, out hit, rayLengthLeader, exitLayer))
            {
                float distance = hit.distance;
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestExit = hit.transform;
                    Debug.DrawRay(rayStart, horizontalDirection * rayLengthLeader, Color.green);
                    Debug.Log($"Señal detectada a {distance} unidades. Ángulo: {horizontalAngle}");
                }
            }
            else
            {
                // Dibujar rayos rojos cuando no detectan nada
                Debug.DrawRay(rayStart, horizontalDirection * rayLengthLeader, Color.red);
            }
        }

        return closestExit;
    }
}









//if (targetExit != null)
//{
//    //Debug.Log("Señal vista!");
//    navMeshAgent.SetDestination(targetExit.position);
//}

//RaycastHit hit;
//if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance))
//{
//    if (hit.collider.CompareTag("ExitSignal"))
//    {
//        Debug.DrawRay(transform.position, transform.forward * raycastDistance, Color.green);
//        setExitDestination();
//    }
//    
//}
//else
//{
//    Debug.DrawRay(transform.position, transform.forward * raycastDistance, Color.red);
//    MoveToRandomDestination();
//}




//Transform FindExitUsingRaycast()
//{
//    Transform closestExit = null;
//    float minDistance = Mathf.Infinity;

//    // Genera rayos en abanico horizontal con inclinación hacia arriba
//    for (int i = 0; i < numberOfRays; i++)
//    {
//        float horizontalAngle = -spreadAngle / 2 + (spreadAngle / (numberOfRays - 1)) * i;
//        // Inclinar el rayo hacia arriba
//        Vector3 direction = Quaternion.Euler(upwardAngle, horizontalAngle, 0) * transform.forward;

//        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, rayLength, exitLayer))
//        {
//            float distance = hit.distance;
//            if (distance < minDistance)
//            {
//                minDistance = distance;
//                closestExit = hit.transform;
//                Debug.DrawRay(transform.position, direction * rayLength, Color.green);
//            }
//        }

//        // Debug Ray (opcional para visualizar los rayos en la escena)
//        Debug.DrawRay(transform.position, direction * rayLength, Color.red);
//    }

//    return closestExit;
//}

//void setExitDestination()
//{
//    if (exitSignObjects != null && exitSignObjects.Length > 0)
//    {
//        Vector3 exitPosition = exitSignObjects[currentExitIndex].transform.position;
//        navMeshAgent.SetDestination(exitPosition);
//        navMeshAgent.speed = agentSpeed; // Set agent speed
//        currentExitIndex = (currentExitIndex + 1) % exitSignObjects.Length;
//    }
//    else
//    {
//        Debug.LogError("No exit sign objects found.");
//    }
//}