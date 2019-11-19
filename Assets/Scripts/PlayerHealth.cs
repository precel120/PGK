using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private int health;
    public int Health { get { return health; } set { health = value; } }
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
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
        }
        if(collision.gameObject.tag == "EnemyFireBall")
        {
            takeDamage(20);
        }
    }

    private void OnTriggerEnter2D(Collider2D collison)
    {
        if(collison.gameObject.tag == "Abyss")
        {
            StartCoroutine(waitBeforeReset());
        }
    }

    public void takeDamage(int damage)
    {
        Health -= damage;
        StartCoroutine(waitDamageTaken());
        if (Health <= 0)
        {
            Health = 0;
            gameObject.GetComponent<PlayerMov>().enabled = false;
            gameObject.GetComponent<PlayerSpell>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            animator.enabled = false;
            StartCoroutine(waitBeforeReset());
        }
    }

    public void heal(int amount)
    {
        if(Health+amount<=100)  Health += amount;
    }

    private IEnumerator waitBeforeReset()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    private IEnumerator waitDamageTaken()
    {
        animator.SetFloat("DamageTaken", 10f);
        yield return new WaitForSeconds(0.15f);
        animator.SetFloat("DamageTaken", 0.0f);
    }
}
