using UnityEngine;

public class ProjectileMov : MonoBehaviour
{
    public float projectileSpeed;
    private float speed = 0.01f;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * projectileSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        /*rb.AddForce(transform.right * speed);
        if(speed < projectileSpeed)
            speed *= Time.deltaTime;*/
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            Destroy(gameObject);
    }
}
