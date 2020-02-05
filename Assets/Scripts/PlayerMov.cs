﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    public float Speed;
    public float JumpHeight;
    public float startTimeBtwAttack;
    public float startTimeBtwKick;
    public Transform attackPos;
    public LayerMask whatisEnemies;
    public float attackRange;

    private BoxCollider2D feet;
    private Rigidbody2D _rigidbody2D;
    private bool faceRight;
    private bool onGround;
    private Animator animator;
    [SerializeField] private Animator windAnim;
    private float timeBtwAttack;
    private float timeBtwKick;
    [SerializeField] private int extraJump;
    private float fallMultiplier = 2.5f;
    private float lowJumpMultiplier = 1f;

    //sound effects
    public AudioClip walkClip;
    private AudioSource playerSource;
    public AudioSource meleeSource;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        feet = gameObject.GetComponent<BoxCollider2D>();
        faceRight = true;
        onGround = true;
        playerSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Move(horizontal);
        Flip(horizontal);

        if(feet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            extraJump = 2;
        }

        if (Input.GetKeyDown("space") || Input.GetKeyDown("w")  && extraJump > 0)
        {
            Jump();
        }

        if(_rigidbody2D.velocity.y < 0)
        {
            _rigidbody2D.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        } else if(_rigidbody2D.velocity.y > 0 && !Input.GetButton ("Jump"))
        {
            _rigidbody2D.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if (timeBtwAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                Debug.Log(meleeSource.clip.ToString());
                meleeSource.Play();
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatisEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    if(enemiesToDamage[i].gameObject.tag=="Enemy") enemiesToDamage[i].GetComponent<FoeMov>().takeDamage(10);
                    if (enemiesToDamage[i].gameObject.tag == "Ghul") enemiesToDamage[i].GetComponent<GhulMov>().takeDamageGhul(10);
                    if (enemiesToDamage[i].gameObject.tag == "Angel") enemiesToDamage[i].GetComponent<AngelMov>().takeDamage(10);
                    if (enemiesToDamage[i].gameObject.tag == "Boss") enemiesToDamage[i].GetComponent<Boss>().takeDamage(10);
                }
                animator.SetBool("IsAttacking", true);
                timeBtwAttack = startTimeBtwAttack;
                //meleeSource.Stop();
            }
            else animator.SetBool("IsAttacking", false);
        }
        else timeBtwAttack -= Time.deltaTime;

        if (timeBtwKick <= 0)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatisEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    if (enemiesToDamage[i].gameObject.tag == "Enemy") enemiesToDamage[i].GetComponent<FoeMov>().takeDamage(10);
                    if (enemiesToDamage[i].gameObject.tag == "Ghul") enemiesToDamage[i].GetComponent<GhulMov>().takeDamageGhul(10);
                    if (enemiesToDamage[i].gameObject.tag == "Angel") enemiesToDamage[i].GetComponent<AngelMov>().takeDamage(10);
                    if (enemiesToDamage[i].gameObject.tag == "Boss") enemiesToDamage[i].GetComponent<Boss>().takeDamage(10);
                }
                animator.SetBool("IsKicking", true);
                timeBtwKick = startTimeBtwKick;
            }
            else animator.SetBool("IsKicking", false);
        }else timeBtwKick -= Time.deltaTime;


        if (!feet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
            windAnim.SetBool("PlayerDoubleJump", false);
            gameObject.transform.Find("Wind").GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void Move(float horizontal)
    {
        if (horizontal == 0)
        {
            playerSource.Stop();
        }
        else if(horizontal != 0 && !playerSource.isPlaying)
        {
            playerSource.clip = walkClip;
            playerSource.Play();
        } 
         _rigidbody2D.velocity = new Vector2(horizontal * Speed, _rigidbody2D.velocity.y);
        animator.SetFloat("Speed",Mathf.Abs(horizontal));
    }

    void Flip(float horizontal)
    {
        if (horizontal > 0 && !faceRight || horizontal < 0 && faceRight)
        {
            faceRight = !faceRight;

            transform.Rotate(0f, 180f, 0f);
        }
    }

    public void PushBack()
    {
        Vector3 pos = gameObject.transform.position;
        if(faceRight)
            _rigidbody2D.transform.localPosition = new Vector3(pos.x - 3f, pos.y);
        else
            _rigidbody2D.transform.localPosition = new Vector3(pos.x + 3f, pos.y);
    }

    void Jump()
    {
        if (extraJump == 1)
        {
            StartCoroutine(doubleJump());
        }

        if (!feet.IsTouchingLayers(LayerMask.GetMask("Ground")) && extraJump <= 0)
            return;

        if (feet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            Vector2 jumpVelocity = new Vector2(0f, JumpHeight);
            _rigidbody2D.velocity = Vector2.up * jumpVelocity;
        }
        else
        {
            Vector2 jumpVelocity = new Vector2(0f, JumpHeight);
            _rigidbody2D.velocity = Vector2.up * jumpVelocity;
            extraJump = 0;
        }

        extraJump--;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
    private IEnumerator doubleJump()
    {
        windAnim.SetBool("PlayerDoubleJump", true);
        gameObject.transform.Find("Wind").GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.4f);
        windAnim.SetBool("PlayerDoubleJump", false);
        gameObject.transform.Find("Wind").GetComponent<SpriteRenderer>().enabled = false;
    }

    public bool getPlayerFaceRight()
    {
        return faceRight;
    }
}
