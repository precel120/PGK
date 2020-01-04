using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float Speed;
    private float health;
    public bool isFrozen;
    public int freezeCounter = 0;
    public GameObject earthScroll;
    public float Health { get { return health; } set { health = value; } }
    private Animator animator;
    public bool FaceRight;

    public Transform fireSpot;
    public GameObject projectile;
    private float spellCooldownTimer = 0.0f;
    public float spellCooldown;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        health = 100f;
        FaceRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFrozen || freezeCounter !=0)
        {
            animator.enabled = true;
            Speed = 2f;
        }
        else if (isFrozen && freezeCounter == 0)
        {
            StartCoroutine(Freeze());
        }
        if (Health >= 50)
        {
            if (Time.time > spellCooldownTimer)
            {
                StartCoroutine(Shooting());
                spellCooldownTimer = Time.time + spellCooldown;
            }
        }
    }

    public void takeDamage(int amount)
    {
        if (health - amount <= 0)
        {
            StartCoroutine(Death());
        }
        else health -= amount;
    }

    void Flip(float horizontal)
    {
        if (horizontal > 0 && !FaceRight || horizontal < 0 && FaceRight)
        {
            FaceRight = !FaceRight;

            transform.Rotate(0f, 180f, 0f);
        }
    }
    IEnumerator Freeze()
    {
        //foeSpell.enabled = false;
        animator.enabled = false;
        Speed = 0f;
        yield return new WaitForSeconds(1.5f);
        freezeCounter = 1;
        isFrozen = true;
    }

    IEnumerator Death()
    {
        animator.SetBool("IsDead", true);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        Instantiate(earthScroll, transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().takeDamage(15);
        }
    }
    IEnumerator Shooting()
    {
        animator.SetBool("Fire", true);
        Instantiate(projectile, fireSpot.position, fireSpot.rotation);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Fire", false);

    }
}
