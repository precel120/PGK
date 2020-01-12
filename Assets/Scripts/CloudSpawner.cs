using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject[] clouds;
    private float randomY;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnCloud", 0f, Random.Range(0.2f, 1.0f));
    }

    private void SpawnCloud()
    {
        randomY = transform.position.y + Random.Range(5.0f, 25.0f);
        int cloud = Random.Range(0, 4);
        Instantiate(clouds[cloud], new Vector3(transform.position.x, randomY), transform.rotation);

    }
}
