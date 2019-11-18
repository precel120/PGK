﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpell : Spell
{
    private string fireballKey = "h";
    private string frostballKey = "k";

    public float fireCooldown;
    private float fireCooldownDuration = 0.0f;
    public float frostCooldown;
    private float frostCooldownDuration = 0.0f;

    private bool canUseFire = false;
    public bool CanUseFire { get { return canUseFire; } set { canUseFire = value; } }
    private bool canUseFrost = false;
    public bool CanUseFrost { get { return canUseFrost; } set { canUseFrost = value; } }

    private FireCooldownMeter fireCooldownMeter;
    private FrostCooldownMeter frostCooldownMeter;

    // Start is called before the first frame update
    void Start()
    {
        fireCooldown = 1.0f;
        frostCooldown = 2.0f;
        fireCooldownMeter = GameObject.FindObjectOfType<FireCooldownMeter>();
        frostCooldownMeter = GameObject.FindObjectOfType<FrostCooldownMeter>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > fireCooldownDuration)
        {
            if (Input.GetKeyDown(fireballKey) && canUseFire)
            {
                Shoot(fireballKey);
                fireCooldownDuration = Time.time + fireCooldown;
                fireCooldownMeter.FlameResetMeter();
            }
        }

        fireCooldownMeter.FlameHandleCooldown(fireCooldown);

        if (Time.time > frostCooldownDuration)
        {
            if (Input.GetKeyDown(frostballKey) && canUseFrost)
            {
                Shoot(frostballKey);
                frostCooldownDuration = Time.time + frostCooldown;
                frostCooldownMeter.FrostResetMeter();
            }
        }

        frostCooldownMeter.FrostHandleCooldown(frostCooldown);
    }

    void Shoot(string key)
    {
        if (key == fireballKey)
        {
            Instantiate(fireballprefab, fireSpot.position, fireSpot.rotation);
        }
        if (key == frostballKey)
        {
            Instantiate(frostballprefab, fireSpot.position, fireSpot.rotation);
        }

    }
}
