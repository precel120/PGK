using UnityEngine;

public class ProjectileMov : MonoBehaviour
{
    public float projectileSpeed;
    private float speed = 0.01f;
    public GameObject waterPrefab;
    public Rigidbody2D rb;
    public ParticleSystem waterExplosion;
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

    void OnCollisionEnter2D(Collision2D collision)
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
        if(gameObject.tag == "EnemyFireBall" && collision.gameObject.tag == "Frostball")
        {
            Instantiate(waterExplosion, gameObject.transform.position, gameObject.transform.rotation);
        }
        Destroy(gameObject);
        
    }

    void RotateOnMov()
    {
        gameObject.transform.Rotate(0, 0, -0.5f);
    }
}
