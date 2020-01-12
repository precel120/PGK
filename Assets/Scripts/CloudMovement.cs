using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    private float speed;
    private float size;
    private Vector3 startingPos;
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        speed = Random.Range(1.0f, 5.0f);
        size = Random.Range(0.2f, 1.0f);
        gameObject.transform.localScale = new Vector3(size, size);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position -= new Vector3(speed * Time.deltaTime, 0f, 0f);
        if(startingPos.x - transform.position.x >= 160.0f)
        {
            Destroy(gameObject);
        }
    }
}
