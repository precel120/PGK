using UnityEngine;

public class ProjectileMov : MonoBehaviour
{
    public float projectileSpeed;
    private float speed = 0.01f;
    public GameObject waterPrefab;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * projectileSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.CompareTag("Water"))
        {
            RotateOnMov();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            if(gameObject.CompareTag("Fireball") && collision.gameObject.CompareTag("Frostball"))
            {
                Instantiate(waterPrefab, gameObject.transform.position, gameObject.transform.rotation);
            }
            Destroy(gameObject);
        }
    }

    void RotateOnMov()
    {
        gameObject.transform.Rotate(0, 0, -0.5f);
    }
}
