using UnityEngine;
using System.Collections;

public class AngelMov : MonoBehaviour
{
    private Animator animator;
    public bool isFrozen = false;
    public bool onFire = false;
    private float moveSpeed;
    private float speed;
    private int currentPoint;
    public int freezeCounter;
    private int health;
    public int Health { get { return health; } set { health = value; } }
    // Use this for initialization
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        freezeCounter = 0;
        currentPoint = 0;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        health = 70;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFrozen || freezeCounter != 0)
        {
            animator.enabled = true;
            speed = 2.5f;
        }
        else if (isFrozen && freezeCounter == 0)
        {
            StartCoroutine(freeze());
        }
        if (Health <= 0)
        {
            StartCoroutine(killFoe());
        }

    }
    public void takeDamage(int damage)
    {
        health -= damage;
    }
    IEnumerator freeze()
    {
        animator.enabled = false;
        speed = 0;
        yield return new WaitForSeconds(2f);
        freezeCounter = 1;
        isFrozen = true;
    }
    IEnumerator killFoe()
    {
        animator.SetBool("IsDead", true);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}