using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeding : MonoBehaviour
{
    public GameObject BigPlant;
    // Start is called before the first frame update
    void Start()
    {
        BigPlant.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            gameObject.SetActive(false);
            BigPlant.SetActive(true);
        }
    }
}
