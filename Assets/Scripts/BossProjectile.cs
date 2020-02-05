using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float projectileSpeed;
    private Rigidbody2D rigidbody;
    private Boss boss;
    public int dmgAmount;
    public bool smallProjectile;
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
        if (smallProjectile && collision.gameObject.CompareTag("Water"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().takeDamage(dmgAmount);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("WallObstacle"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Fireball") || collision.gameObject.CompareTag("Water") || collision.gameObject.CompareTag("Frostball"))
        {
            Destroy(collision.gameObject);
        }
    }
}
