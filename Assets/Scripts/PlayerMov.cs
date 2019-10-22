using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    public float Speed;
    public float JumpHeight;
    public float startTimeBtwAttack;
    public Transform attackPos;
    public LayerMask whatisEnemies;
    public float attackRange;

    private Rigidbody2D _rigidbody2D;
    private bool faceRight;
    private bool onGround;
    private Animator animator;
    private float timeBtwAttack;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        faceRight = true;
        onGround = true;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Move(horizontal);
        Flip(horizontal);

        if (Input.GetKeyDown("space")) Jump();

        if (timeBtwAttack <= 0)
        {
            if (Input.GetKey(KeyCode.J))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatisEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<FoeMov>().takeDamage(12);
                }
                animator.SetBool("IsAttacking", true);
                timeBtwAttack = startTimeBtwAttack;
            }
            else animator.SetBool("IsAttacking", false);
        }
        else timeBtwAttack -= Time.deltaTime;
    }

    void Move(float horizontal)
    {
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

    void Jump()
    {
        onGround = false;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, JumpHeight), ForceMode2D.Impulse);
        animator.SetBool("Jump", true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            onGround = true;
            animator.SetBool("Jump", false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
