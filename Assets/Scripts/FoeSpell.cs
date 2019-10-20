using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoeSpell : MonoBehaviour
{
    public Transform fireSpot;
    public GameObject fireballprefab;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Fire", 1f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Fire()
    {
        Instantiate(fireballprefab, fireSpot.position, fireSpot.rotation);
    }
}
