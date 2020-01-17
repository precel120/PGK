using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float Speed;
    private float health;
    public float Health { get { return health; } set { health = value; } }
    public bool canShoot;
    public bool canHugeShoot;
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

    public GameObject player;
    public ShieldHandler shieldHandler;

    public GameObject closeWall;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        health = 500f;
        FaceRight = true;
        canShoot = false;
        canHugeShoot = false;
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        if (Health >= 400)
        {
            if (Time.time > spellCooldownTimer && canShoot)
            {
                StartCoroutine(Shooting());
                spellCooldownTimer = Time.time + spellCooldown;
            }else if(Time.time > hugeSpellCooldownTimer && canHugeShoot)
            {
                StartCoroutine(HugeShooting());
                hugeSpellCooldownTimer = Time.time + hugeSpellCooldown;
            }
        }else if (Health < 400 && Health > 0)
        {
            shieldHandler.removeShield();
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Speed * Time.deltaTime);
            Flip();
            float distanceToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);
            if(distanceToPlayer <= 10)
            {
                StartCoroutine(Melee());
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

    void Flip()
    {
        Vector3 distance = gameObject.transform.position - player.transform.position;
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

    IEnumerator Death()
    {
        health = 0;
        animator.SetBool("IsDead", true);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        closeWall.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().takeDamage(10);
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
        yield return new WaitForSeconds(1f);
        animator.SetBool("Fire", false);
        yield return new WaitForSeconds(2f);
        canShoot = true;
    }

    IEnumerator Spawn()
    {
        canHugeShoot = false;
        canShoot = false;
        yield return new WaitForSeconds(3f);
        canShoot = true;
        yield return new WaitForSeconds(5f);
        canHugeShoot = true;
    }

    IEnumerator Melee()
    {
        int choice = Random.Range(0, 3);
        switch (choice)
        {
            case 0:
                animator.SetBool("useSword", true);
                yield return new WaitForSeconds(1f);
                player.GetComponent<PlayerHealth>().takeDamage(20);
                animator.SetBool("useSword", false);
                break;
            case 1:
                animator.SetBool("Uppercut", true);
                yield return new WaitForSeconds(1f);
                player.GetComponent<PlayerHealth>().takeDamage(10);
                player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 20);
                animator.SetBool("Uppercut", false);
                break;
            case 2:
                animator.SetBool("useFist", true);
                yield return new WaitForSeconds(1f);
                player.GetComponent<PlayerHealth>().takeDamage(5);
                player.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 20);
                animator.SetBool("useFist", false);
                break;
        }
    }
}
