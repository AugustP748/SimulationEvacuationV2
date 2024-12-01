using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EvacuationSImulation : MonoBehaviour
{
    public GameObject bigExplosionPrefab;
    public NavMeshAgent agent;
    public GameObject firePrefab;
    //[SerializeField] private StartButton startButton;

    // Start is called before the first frame update
    public void StartSimulation()
    {
        SpawnBigExplosion();
        //StartCoroutine(SpawnFire());
    }

    public void SpawnBigExplosion()
    {
        Vector3 randomPosition = GetRandomNavMeshPosition();
        Instantiate(bigExplosionPrefab, randomPosition, Quaternion.identity);
        Instantiate(firePrefab, randomPosition, Quaternion.identity); // Spawning WildFire at the same position
    }

    Vector3 GetRandomNavMeshPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 50;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, 50, 1);
        return new Vector3(hit.position.x, 2.84f, hit.position.z);
    }

IEnumerator SpawnFire()
    {
        yield return new WaitForSeconds(1);
        Vector3 firePosition = GetRandomFirePosition();
        Instantiate(firePrefab, firePosition, Quaternion.identity);
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

        return new Vector3(x, 0.4f, z);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Start()
    {
       
    }
}