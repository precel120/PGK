using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpell : MonoBehaviour
{
    public Transform fireSpot;
    public GameObject fireballprefab;
    public GameObject frostballprefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            Shoot("1");
        }
        else if (Input.GetKeyDown("2"))
        {
            Shoot("2");
        }
    }

    void Shoot(string key)
    {
        if (key == "1")
        {
            Instantiate(fireballprefab, fireSpot.position, fireSpot.rotation);
        }
        if (key == "2")
        {
            Instantiate(frostballprefab, fireSpot.position, fireSpot.rotation);
        }

    }
}
