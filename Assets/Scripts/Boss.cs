﻿using System.Collections;
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
    public GameObject hpBar;

    public Transform swordPoint;
    public Transform uppercutPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;
    private float lastAttackTime = 0;
    public float attackDelay;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        health = 350f;
        FaceRight = true;
        canShoot = false;
        canHugeShoot = false;
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        if (Health > 250)
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
        }else if (Health <= 250 && Health > 0)
        {
            shieldHandler.removeShield();
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Speed * Time.deltaTime);
            Flip();
            float distanceToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);
            if (distanceToPlayer <= 2.5)
            {
                if (Time.time > lastAttackTime)
                {
                    StartCoroutine(Melee(1));
                    lastAttackTime = Time.time + attackDelay;
                }
            }else if (distanceToPlayer <= 4)
            {
                if (Time.time > lastAttackTime)
                {
                    StartCoroutine(Melee(0));
                    lastAttackTime = Time.time + attackDelay;
                }
            }else if (distanceToPlayer > 8)
            {
                if (Time.time > spellCooldownTimer && canShoot)
                {
                    StartCoroutine(Shooting());
                    spellCooldownTimer = Time.time + spellCooldown;
                }
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
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().sortingOrder = -1;
        canHugeShoot = false;
        canShoot = false;
        Speed = 0;
        closeWall.SetActive(false);
        hpBar.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(Health > 250) collision.gameObject.GetComponent<PlayerHealth>().takeDamage(15);
            else collision.gameObject.GetComponent<PlayerHealth>().takeDamage(5);
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
        yield return new WaitForSeconds(0.8f);
        animator.SetBool("Fire", false);
        yield return new WaitForSeconds(1.5f);
        canShoot = true;
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1.5f);
        canShoot = true;
    }

    IEnumerator Melee(int choice)
    {
        Collider2D[] hitPlayer;
        switch (choice)
        {
            case 0:
                animator.SetBool("useSword", true);
                yield return new WaitForSeconds(0.5f);
                animator.SetBool("useSword", false);
                hitPlayer = Physics2D.OverlapCircleAll(swordPoint.position, attackRange, playerLayer);
                foreach (Collider2D play in hitPlayer)
                {
                    player.GetComponent<PlayerHealth>().takeDamage(15);
                }
                break;
            case 1:
                animator.SetBool("Uppercut", true);
                yield return new WaitForSeconds(0.4f);
                animator.SetBool("Uppercut", false);
                hitPlayer = Physics2D.OverlapCircleAll(uppercutPoint.position, attackRange, playerLayer);
                foreach (Collider2D play in hitPlayer)
                {
                    player.GetComponent<PlayerHealth>().takeDamage(10);
                }
                break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (uppercutPoint == null || swordPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(uppercutPoint.position, attackRange);
        Gizmos.DrawWireSphere(swordPoint.position, attackRange);
    }
}
