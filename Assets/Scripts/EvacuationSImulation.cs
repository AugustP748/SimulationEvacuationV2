using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class EvacuationSImulation : MonoBehaviour
{
    public GameObject bigExplosionPrefab;
    public GameObject firePrefab;
    public GameObject leaderPrefab;
    public GameObject followerPrefab;
    public GameObject explorerPrefab;
    public GameObject panicStrickerPrefab;
    //[SerializeField] private StartButton startButton;
    public NavMeshSurface surface; // Necesitas tener este componente en un objeto de la escena

    // Start is called before the first frame update
    public void StartSimulation()
    {
        SpawnBigExplosion();
        //StartCoroutine(SpawnFire());
    }

    public void SpawnAgents(int cantAgents)
    {
        for (int i = 0; i < cantAgents; i++)
        {
            float randomValue = Random.value;
            GameObject agentPrefab;

            if (randomValue < 0.2f) // 20% chance for leader
            {
                agentPrefab = leaderPrefab;
            }
            else if (randomValue < 0.7f) // 50% chance for follower
            {
                agentPrefab = followerPrefab;
            }
            else if (randomValue < 0.9f) // 20% chance for explorer
            {
                agentPrefab = explorerPrefab;
            }
            else // 10% chance for panic stricker
            {
                agentPrefab = panicStrickerPrefab;
            }

            Vector3 randomPosition = GetRandomNavMeshPosition();
            randomPosition.y = -0.8f;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPosition, out hit, 1f, NavMesh.AllAreas))
            {
                Instantiate(agentPrefab, hit.position, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("Failed to create agent because it is not close enough to the NavMesh");
            }
        }
    }

    public void SpawnBigExplosion()
    {
        StartCoroutine(SpawnBigExplosionDelayed());
    }

    IEnumerator SpawnBigExplosionDelayed()
    {
        yield return new WaitForSeconds(5);
        Vector3 randomPosition = GetRandomNavMeshPosition();

        Instantiate(bigExplosionPrefab, randomPosition, Quaternion.identity);
        Instantiate(firePrefab, randomPosition, Quaternion.identity); // Spawning WildFire at the same position
        // Encuentra todos los objetos con el tag especificado
        GameObject[] objectsToModify = GameObject.FindGameObjectsWithTag("Obstacle");

        foreach (GameObject obj in objectsToModify)
        {
            obj.isStatic = false; // Desactiva el estado estático


            // Desactivar NavMeshObstacle si existe
            NavMeshObstacle obstacle = obj.GetComponent<NavMeshObstacle>();
            if (obstacle != null)
            {
                obstacle.enabled = false;
            }

            // Agregar o configurar Rigidbody
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = obj.AddComponent<Rigidbody>();
                rb.mass = 0.1f;
            }

            rb.mass = 0.1f;
            //rb.isKinematic = false;
            //rb.useGravity = true;
            //rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

            //// Configurar colliders
            //Collider[] colliders = obj.GetComponents<Collider>();
            //foreach (Collider col in colliders)
            //{
            //    col.enabled = true;
            //}

            //NavMeshSurface surface = FindObjectOfType<NavMeshSurface>();
            //if (surface != null)
            //{
            //    surface.BuildNavMesh();
            //}
        }
    }

    Vector3 GetRandomNavMeshPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 50;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, 50, 1);
        return new Vector3(hit.position.x, 1f, hit.position.z);
    }

    Vector3 GetRandomFirePosition()
    {
        int zone = Random.Range(1, 7);
        float x = 0, z = 0;

        switch (zone)
        {
            case 1: //sala1 depto alum
                x = Random.Range(-59f, -44f);
                z = Random.Range(-23f, -14f);
                break;
            case 2: //sala2 depto alum
                x = Random.Range(-43f, -30f);
                z = Random.Range(-23f, -14f);
                break;
            case 3: //sala general depto alum
                x = Random.Range(-59f, -30f);
                z = Random.Range(-11f, 2f);
                break;
            case 4: //sala de estudio
                x = Random.Range(-59f, -41f);
                z = Random.Range(4f, 18f);
                break;
            case 5: //159
                x = Random.Range(-59f, -29f);
                z = Random.Range(20f, 35f);
                break;
            case 6: //157
                x = Random.Range(-27f, 0f);
                z = Random.Range(20f, 35f);
                break;
            case 7: //155
                x = Random.Range(2f, 26f);
                z = Random.Range(20f, 35f);
                break;
            case 8: //153 lab 1
                x = Random.Range(28f, 60f);
                z = Random.Range(20f, 35f);
                break;
            case 9: //154 lab 2
                x = Random.Range(26f, 54f);
                z = Random.Range(5f, -10f);
                break;
            case 10: //156
                x = Random.Range(-1f, 24f);
                z = Random.Range(5f, -10f);
                break;
            case 11: //158
                x = Random.Range(-3f, -28f);
                z = Random.Range(5f, -10f);
                break;
        }

        return new Vector3(x, -1f, z);
    }

}
