using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCloud : MonoBehaviour
{
    public float speed;
    private Vector3 startingPos;
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position -= new Vector3(speed * Time.deltaTime, 0f, 0f);
        if (startingPos.x - transform.position.x >= 160.0f)
        {
            Destroy(gameObject);
        }
    }
}
