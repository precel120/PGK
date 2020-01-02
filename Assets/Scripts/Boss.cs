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
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        health = 20f;
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
            StartCoroutine(freeze());
        }
    }

    public void takeDamage(int amount)
    {
        if (health - amount <= 0)
        {
            StartCoroutine(death());
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
    IEnumerator freeze()
    {
        //foeSpell.enabled = false;
        animator.enabled = false;
        Speed = 0f;
        yield return new WaitForSeconds(1.5f);
        freezeCounter = 1;
        isFrozen = true;
    }

    IEnumerator death()
    {
        animator.SetBool("IsDead", true);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        Instantiate(earthScroll, transform.position, Quaternion.identity);
    }
}
