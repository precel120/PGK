using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMov : MonoBehaviour
{
    public float projectileSpeed;
    public GameObject waterPrefab;
    public Rigidbody2D rb;
    public ParticleSystem waterExplosion;
    private float lifeTime = 2.0f;
    private Vector3 startingPos;
    // Start is called before the first frame update
    void Start()
    {
        if (!gameObject.CompareTag("EarthSpell"))
        {
            rb.velocity = transform.right * projectileSpeed;
        }
        else
        {
            startingPos = gameObject.transform.position;
            rb.AddForce(Vector3.up * 150);
        }

        if(gameObject.CompareTag("Fireball"))
        {
            lifeTime = 2.0f;
        }
        else if(gameObject.CompareTag("Frostball"))
        {
            lifeTime = 5.0f;
        }
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(gameObject.CompareTag("Water"))
        {
            RotateOnMov(-0.5f);
        }
        if (gameObject.CompareTag("Frostball"))
        {
            RotateOnMov(2.0f);
        }
        if(gameObject.CompareTag("EarthSpell"))
        {
            if(gameObject.transform.position.y - startingPos.y > 1)
            {
                rb.velocity = Vector3.zero;
                rb.velocity = transform.right * projectileSpeed;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(gameObject.CompareTag("Fireball") && collision.gameObject.CompareTag("Frostball"))
        {
            Instantiate(waterPrefab, gameObject.transform.position, gameObject.transform.rotation);
        }
        if (gameObject.tag == "Fireball" && collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<FoeMov>().takeDamage(20);
            //if (collision.gameObject.GetComponent<FoeMov>().Health <= 0)
           // collision.gameObject.GetComponent<FoeMov>().onFire = true;
        }
        else if (gameObject.tag == "Fireball" && collision.gameObject.tag == "Boss")
        {
            collision.gameObject.GetComponent<Boss>().takeDamage(20);
        }
        if (gameObject.tag == "Frostball" && collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<FoeMov>().takeDamage(5);
            if(collision.gameObject.GetComponent<FoeMov>().freezeCounter==0)
            collision.gameObject.GetComponent<FoeMov>().isFrozen = true;
        }else if (gameObject.tag == "Frostball" && collision.gameObject.tag == "Boss")
        {
            collision.gameObject.GetComponent<Boss>().takeDamage(5);
        }
        if (gameObject.tag == "Fireball" && collision.gameObject.tag == "Ghul")
        {
            collision.gameObject.GetComponent<GhulMov>().takeDamageGhul(20);
        }
        if (gameObject.tag == "Fireball" && collision.gameObject.tag == "Angel")
        {
            collision.gameObject.GetComponent<AngelMov>().takeDamage(20);
        }
        if (gameObject.tag == "Water" && collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<FoeMov>().takeDamage(30);
        }
        else if (gameObject.tag == "Water" && collision.gameObject.tag == "Boss")
        {
            collision.gameObject.GetComponent<Boss>().takeDamage(30);
        }
        if (gameObject.tag == "EnemyFireBall" && collision.gameObject.tag == "Frostball")
        {
            Instantiate(waterExplosion, gameObject.transform.position, gameObject.transform.rotation);
        }
        if(gameObject.tag == "EarthSpell" && collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<FoeMov>().takeDamage(50);
        }
        Destroy(gameObject);
    }

    void RotateOnMov(float value)
    {
        gameObject.transform.Rotate(0, 0, value);
    }
}
