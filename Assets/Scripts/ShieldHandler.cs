using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHandler : MonoBehaviour
{
    private int shieldCounter;
    private CircleCollider2D circleCollider;
    private Boss boss;
    // Start is called before the first frame update
    void Start()
    {
        shieldCounter = 0;
        circleCollider = GetComponent<CircleCollider2D>();
        boss = GetComponentInParent<Boss>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shieldCounter == 6)
        {
            StartCoroutine(shieldDestroyed());
        }
    }
    public void add()
    {
        shieldCounter++;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Destroy(collision.gameObject);
        }
    }

    IEnumerator shieldDestroyed()
    {
        shieldCounter = 0;
        circleCollider.enabled = false;
        boss.canShoot = false;
        boss.canHugeShoot = false;
        yield return new WaitForSeconds(4f);
        circleCollider.enabled = true;
        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<BossShield>().resetAngle();
            child.gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(1.5f);
        boss.canShoot = true;
        yield return new WaitForSeconds(2.5f);
        boss.canHugeShoot = true;
    }

    public void removeShield()
    {
        gameObject.SetActive(false);
    }
}
