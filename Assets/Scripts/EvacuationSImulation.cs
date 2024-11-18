using System.Collections;
using UnityEngine;

public class EvacuationSImulation : MonoBehaviour
{
    public GameObject firePrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnFire());
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
            case 1:
                x = Random.Range(-22f, 2f);
                z = Random.Range(-20f, -5f);
                break;
            case 2:
                x = Random.Range(-22f, 2f);
                z = Random.Range(11f, 26f);
                break;
            case 3:
                x = Random.Range(6f, 30f);
                z = Random.Range(11f, 26f);
                break;
            case 4:
                x = Random.Range(6f, 30f);
                z = Random.Range(-20f, -5f);
                break;
            case 5:
                x = Random.Range(33f, 57f);
                z = Random.Range(-20f, -5f);
                break;
            case 6:
                x = Random.Range(33f, 57f);
                z = Random.Range(11f, 26f);
                break;
        }

        return new Vector3(x, 0.4f, z);
    }

    // Update is called once per frame
    void Update()
    {

    }
}