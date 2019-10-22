using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image content;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandleBar();
    }

    private void HandleBar()
    {
        content.fillAmount = MapHealth(player.GetComponent<PlayerHealth>().Health, 0, 100, 0, 1);
    }

    private float MapHealth(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        //Script for normalizing health value to [0 - 1] range
        //Ex.  ( 80   -   0 )  *  (1      -     0) / ( 100  -   0  ) +    0;
    }
}
