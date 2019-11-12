using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownMeter : MonoBehaviour
{
    [SerializeField]
    private Image content;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleCooldown(float amount)
    {
        content.fillAmount += 1 / amount * Time.deltaTime;
    }

    public void ResetMeter()
    {
        content.fillAmount = 0;
    }
}
