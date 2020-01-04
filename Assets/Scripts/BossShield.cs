using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShield : MonoBehaviour
{
    [SerializeField]
    Transform rotationCenter;
    [SerializeField]
    float rotationRadius = 2f, angularSpeed = 3f;
    float posX, posY;
    public float angle;
    private float baseAngle;
    private ShieldHandler shieldHandler;
    // Start is called before the first frame update
    void Start()
    {
        shieldHandler = GetComponentInParent<ShieldHandler>();
        baseAngle = angle;
    }

    // Update is called once per frame
    void Update()
    {
        posX = rotationCenter.position.x + Mathf.Cos(angle) * rotationRadius;
        posY = rotationCenter.position.y + Mathf.Sin(angle) * rotationRadius;
        transform.position = new Vector2(posX, posY);
        angle = angle + Time.deltaTime * angularSpeed;
        if (angle >= 360)
        {
            angle = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().takeDamage(10);
        }else if (collision.gameObject.CompareTag("Frostball"))
        {
            Destroy(collision.gameObject);
            gameObject.SetActive(false);
            shieldHandler.add();
        }
    }

    public void resetAngle()
    {
        angle = baseAngle;
    }
}
