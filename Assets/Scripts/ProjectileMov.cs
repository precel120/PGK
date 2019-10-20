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
            if (gameObject.tag == "Fireball" && collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<FoeMov>().takeDamage(10);
            }
            if (gameObject.tag == "Frostball" && collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<FoeMov>().takeDamage(15);
            }
            if (gameObject.tag == "Water" && collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<FoeMov>().takeDamage(30);
            }
            Destroy(gameObject);
        }
        
    }

    void RotateOnMov()
    {
        gameObject.transform.Rotate(0, 0, -0.5f);
    }
}
