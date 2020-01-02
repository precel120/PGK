using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float projectileSpeed;
    private Rigidbody2D rigidbody;
    private Boss boss;
    // Start is called before the first frame update
    void Start()
    {
        boss = GetComponent<Boss>();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        rigidbody.velocity = transform.right * projectileSpeed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().Health -= 20;
        }
        Destroy(gameObject);
    }
}
