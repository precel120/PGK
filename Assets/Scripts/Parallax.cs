using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float lenght, startPos;
    public GameObject cam;
    public float parallaxEffect;
    public bool isUnderground;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        try
        {
            lenght = GetComponent<SpriteRenderer>().bounds.size.x;
        } catch {
            Debug.Log("Object doesnt have Sprite Renderer");
            }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dist = (cam.transform.position.x * parallaxEffect);
        float temp = (cam.transform.position.x * (1 - parallaxEffect));

        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        if (temp > startPos + lenght) startPos += lenght;
        else if (temp < startPos - lenght) startPos -= lenght;
    }

    public void setParallax(float amount)
    {
        parallaxEffect = amount;
    }
}
