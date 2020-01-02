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

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        feet = gameObject.GetComponent<BoxCollider2D>();
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

        if(feet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            extraJump = 2;
        }

        if (Input.GetKeyDown("space") && extraJump > 0)
        {
            Jump();
        }

        if (timeBtwAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
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

        CapsuleCollider2D capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        CapsuleCollider2D tempCapsule = capsuleCollider2D;
        if (Input.GetKeyDown(KeyCode.C))
        {
            Speed = 0;
            animator.SetBool("IsCrouching", true);
            capsuleCollider2D.size *= 0.5f;
            capsuleCollider2D.offset *= 2.5f;
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            animator.SetBool("IsCrouching", false);
            capsuleCollider2D.size /= 0.5f;
            capsuleCollider2D.offset /= 2.5f;
            Speed = 10;
        }
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
            _rigidbody2D.velocity += jumpVelocity;
            animator.SetBool("Jump", true);
        }
        else
        {
            Vector2 jumpVelocity = new Vector2(0f, JumpHeight);
            _rigidbody2D.velocity += jumpVelocity;
            animator.SetBool("Jump", true);
            extraJump = 0;
        }

        extraJump--;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            animator.SetBool("Jump", false);
            if (feet.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                windAnim.SetBool("PlayerDoubleJump", false);
                gameObject.transform.Find("Wind").GetComponent<SpriteRenderer>().enabled = false;
            }
        }
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
