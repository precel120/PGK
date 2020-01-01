using UnityEngine;
using System.Collections;

public class FoeMov : MonoBehaviour
{
    private FoeSpell foeSpell;
    // public Transform[] points;
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
        foeSpell = gameObject.GetComponent<FoeSpell>();
        freezeCounter = 0;
        // transform.position = points[0].position;
        currentPoint = 0;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        health = 45;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFrozen || freezeCounter != 0)
        {
            animator.enabled = true;
            speed = 2.5f;
            foeSpell.enabled = true;
            //if (transform.position == points[currentPoint].position)
            //{
            //    transform.Rotate(0f, 180f, 0f);
            //    currentPoint++;
            //}
            //if (currentPoint >= points.Length)
            //{
            //    currentPoint = 0;
            //}
        }
        else if (isFrozen && freezeCounter == 0)
        {
            StartCoroutine(freeze());
        }
        // transform.position = Vector2.MoveTowards(transform.position, points[currentPoint].position, moveSpeed = Time.deltaTime * speed);
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
        foeSpell.enabled = false;
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