using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeding : MonoBehaviour
{
    public GameObject gameObject1;
    // Start is called before the first frame update
    void Start()
    {
        gameObject1.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            gameObject.SetActive(false);
            gameObject1.SetActive(true);
        }
    }
}
