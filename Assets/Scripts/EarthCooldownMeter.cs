using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EarthCooldownMeter : MonoBehaviour
{
    [SerializeField]
    private Image earth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EarthHandleCooldown(float amount)
    {
        earth.fillAmount += 1 / amount * Time.deltaTime;
    }

    public void EarthResetMeter()
    {
        earth.fillAmount = 0;
    }
}
