using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrostCooldownMeter : MonoBehaviour
{
    [SerializeField]
    private Image frost;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FrostHandleCooldown(float amount)
    {
        frost.fillAmount += 1 / amount * Time.deltaTime;
    }

    public void FrostResetMeter()
    {
        frost.fillAmount = 0;
    }
}
