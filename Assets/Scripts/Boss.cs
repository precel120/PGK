using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float Speed;
    private float health;
    public bool isFrozen;
    public bool canShoot;
    public bool canHugeShoot;
    public int freezeCounter = 0;
    public GameObject earthScroll;
    public float Health { get { return health; } set { health = value; } }
    private Animator animator;
    public bool FaceRight;

    public Transform fireSpot;
    public Transform hugeFireSpot;
    public GameObject projectile;
    public GameObject hugeProjectile;
    private float spellCooldownTimer = 0.0f;
    public float spellCooldown;
    private float hugeSpellCooldownTimer = 0.0f;
    public float hugeSpellCooldown;

    public Transform player;
    public ShieldHandler shieldHandler;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        health = 55f;
        FaceRight = true;
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFrozen || freezeCounter !=0)
        {
            animator.enabled = true;
            canShoot = true;
            Speed = 2f;
        }
        else if (isFrozen && freezeCounter == 0)
        {
            StartCoroutine(Freeze());
        }
        if (Health >= 50)
        {
            if (Time.time > spellCooldownTimer && canShoot)
            {
                StartCoroutine(Shooting());
                spellCooldownTimer = Time.time + spellCooldown;
            }else if(Time.time > hugeSpellCooldownTimer)
            {
                StartCoroutine(HugeShooting());
                hugeSpellCooldownTimer = Time.time + hugeSpellCooldown;
            }
        }else if (Health < 50 && Health > 0)
        {
            shieldHandler.removeShield();
            transform.position = Vector3.MoveTowards(transform.position, player.position, Speed * Time.deltaTime);
            Flip();
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

    void Flip()
    {
        Vector3 distance = gameObject.transform.position - player.position;
        if (distance.x > 0 && FaceRight)
        {
            FaceRight = !FaceRight;
            transform.Rotate(0f, 180f, 0f);
        }else if (distance.x < 0 && !FaceRight)
        {
            FaceRight = !FaceRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }
    IEnumerator Freeze()
    {
        canShoot = false;
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

    IEnumerator HugeShooting()
    {
        canShoot = false;
        animator.SetBool("Fire", true);
        Instantiate(hugeProjectile, hugeFireSpot.position, hugeFireSpot.rotation);
        yield return new WaitForSeconds(2f);
        animator.SetBool("Fire", false);
        canShoot = true;
    }

    IEnumerator Spawn()
    {
        //canHugeShoot = false;
        canShoot = false;
        yield return new WaitForSeconds(3f);
        canShoot = true;
        //yield return new WaitForSeconds(5f);
        //canHugeShoot = true;
    }
}
