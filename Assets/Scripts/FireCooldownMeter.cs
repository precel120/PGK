using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireCooldownMeter : MonoBehaviour
{
    [SerializeField]
    private Image flame;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FlameHandleCooldown(float amount)
    {
        flame.fillAmount += 1 / amount * Time.deltaTime;
    }

    public void FlameResetMeter()
    {
        flame.fillAmount = 0;
    }
}
