using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private int health;
    public int Health { get { return health; } set { health = value; } }
    // Start is called before the first frame update
    void Start()
    {
        Health = 100;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            takeDamage(5);
            //animator.SetFloat("DamageTaken", 10f);
        }
        if(collision.gameObject.tag == "EnemyFireBall")
        {
            takeDamage(20);
        }
    }

    public void takeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Health = 0;
            gameObject.SetActive(false);
        }
    }
}
