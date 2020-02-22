using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BossTrigger : MonoBehaviour
{
    public GameObject boss;
    public GameObject wall;
    public GameObject bossHP;
    public GameObject closeWall;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            boss.SetActive(true);
            wall.SetActive(true);
            bossHP.SetActive(true);
            closeWall.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
