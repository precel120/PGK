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
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        rigidbody.velocity = transform.right * projectileSpeed;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().takeDamage(15);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("WallObstacle") || collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("EarthSpell") || collision.gameObject.CompareTag("Fireball") || collision.gameObject.CompareTag("Water") || collision.gameObject.CompareTag("Frostball"))
        {
            Destroy(collision.gameObject);
        }
    }
}
