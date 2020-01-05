using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Tilemaps;
using Cinemachine;

public class BossTrigger : MonoBehaviour
{
    public GameObject boss;
    public GameObject wall;
    public GameObject bossHP;
    public CinemachineVirtualCamera vcam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            boss.SetActive(true);
            wall.SetActive(true);
            bossHP.SetActive(true);
            gameObject.SetActive(false);
            vcam.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = 20;
        }
    }
}
