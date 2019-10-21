using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoeSpell : Spell
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Fire", 0.5f, 3f);
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
