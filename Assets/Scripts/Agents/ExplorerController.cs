using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ExplorerAgent : AgentController
{
    public Transform[] pointsOfInterest; // Array de puntos para explorar
    private int currentPointIndex = 0;  // �ndice del punto actual
    private bool bClosestPoint = false; // Indica si el punto m�s cercano fue encontrado
    private int closestPointIndex = -1; // �ndice del punto m�s cercano
    //private bool foundExit = false;
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void PerformBehavior()
    {
        //Debug.Log("Explorador en acci�n...");

        //MoveToRandomDestination();
        MoveToPosition();

        //MoveToExit();
        //if (CheckForExit())
        //{
        //    Debug.Log("encontr� la salida");
        //}
        //else
        //{
        //    MoveToPosition();
        //    Debug.Log("explorando...");
        //}
    }
    public void MoveToPosition()
    {
        if (navMeshAgent.remainingDistance > 1f || navMeshAgent.pathPending)
            return;

        if (currentPointIndex == pointsOfInterest.Length)
        {
            Debug.Log("Explorador ha terminado de explorar.");
            MoveToExit();
            return;
        }

        // Encuentra la posici�n m�s cercana al agente
        Vector3 position = NavMesh.SamplePosition(transform.position, out NavMeshHit hit, wanderRadius, NavMesh.AllAreas) ? hit.position : transform.position;

        if(!bClosestPoint)
        {
            float closestDistance = float.MaxValue;
            for (int i = 0; i < pointsOfInterest.Length; i++)
            {
                float distance = Vector3.Distance(pointsOfInterest[i].position, position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPointIndex = i;
                }
            }
            bClosestPoint = true;
        }
        // Encuentra el �ndice del punto m�s cercano
        
        //Debug.Log($"Punto m�s cercano: {closestPointIndex}");

        // Itera sobre los puntos de inter�s a partir del punto m�s cercano
        for (int i = closestPointIndex; i < pointsOfInterest.Length; i++)
        {
            Vector3 point = pointsOfInterest[i].position;
            
            if (NavMesh.SamplePosition(point, out NavMeshHit pointHit, wanderRadius, NavMesh.AllAreas))
            {
                //Debug.Log($"Punto set: {i}");
                navMeshAgent.SetDestination(pointHit.position);
                currentPointIndex = i + 1;
                closestPointIndex = i + 1;
                break;
            }
        }
    }

    //void CheckForExit()
    //{
    //    GameObject exitObject = GameObject.FindGameObjectWithTag("Exit");
    //    if (exitObject != null)
    //    {
    //        Vector3 exitPosition = exitObject.transform.position;
    //        navMeshAgent.SetDestination(exitPosition);
    //    }
    //}

    public void MoveToExit()
    {

        GameObject exit = GameObject.FindGameObjectWithTag("Exit");
        if (exit != null)
        {
            Vector3 exitPosition = exit.transform.position;
            navMeshAgent.SetDestination(exitPosition);
            Debug.Log($"{gameObject.name} detect� una salida y se dirige hacia ella.");
        }
    }


}
