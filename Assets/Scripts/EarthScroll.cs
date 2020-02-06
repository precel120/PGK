using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EarthScroll : MonoBehaviour
{
    public Image locked;

    private void Start()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<PlayerSpell>().CanUseEarth = true;
            locked.enabled = false;
        }
    }
}
