using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireScroll : MonoBehaviour
{
    public Image locked;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<PlayerSpell>().CanUseFire = true;
            GameObject.FindObjectOfType<ChangeText>().TextChange("Fireball - 'H'");
            locked.enabled = false;
        }
    }
}
