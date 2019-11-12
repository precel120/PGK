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
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * projectileSpeed;
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
    void Update()
    {
        if(gameObject.CompareTag("Water"))
        {
            RotateOnMov(-0.5f);
        }
        if (gameObject.CompareTag("Frostball"))
        {
            RotateOnMov(2.0f);
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
            if (collision.gameObject.GetComponent<FoeMov>().Health <= 0)
            collision.gameObject.GetComponent<FoeMov>().onFire = true;
        }
        if (gameObject.tag == "Frostball" && collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<FoeMov>().takeDamage(5);
            if(collision.gameObject.GetComponent<FoeMov>().freezeCounter==0)
            collision.gameObject.GetComponent<FoeMov>().isFrozen = true;
        }
        if (gameObject.tag == "Water" && collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<FoeMov>().takeDamage(30);
        }
        if(gameObject.tag == "EnemyFireBall" && collision.gameObject.tag == "Frostball")
        {
            Instantiate(waterExplosion, gameObject.transform.position, gameObject.transform.rotation);
        }
        Destroy(gameObject);
        
    }

    void RotateOnMov(float value)
    {
        gameObject.transform.Rotate(0, 0, value);
    }
}
