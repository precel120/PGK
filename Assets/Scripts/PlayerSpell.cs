﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpell : Spell
{
    private string fireballKey = "h";
    private string frostballKey = "k";
    private string earthSpellKey = "u";

    private float fireCooldown;
    private float fireCooldownDuration = 0.0f;
    private float frostCooldown;
    private float frostCooldownDuration = 0.0f;
    private float earthCooldown;
    private float earthCooldownDuration = 0.0f;

    private bool canUseFire = true;
    public bool CanUseFire { get { return canUseFire; } set { canUseFire = value; } }
    private bool canUseFrost = true;
    public bool CanUseFrost { get { return canUseFrost; } set { canUseFrost = value; } }
    private bool canUseEarth = true;
    public bool CanUseEarth { get { return canUseEarth; } set { canUseEarth = value; } }

    private FireCooldownMeter fireCooldownMeter;
    private FrostCooldownMeter frostCooldownMeter;
    private EarthCooldownMeter earthCooldownMeter;

    public AudioSource earthSource;
    public AudioSource fireSource;

    // Start is called before the first frame update
    void Start()
    {
        fireCooldown = 1.0f;
        frostCooldown = 2.0f;
        earthCooldown = 5.0f;

        fireCooldownMeter = GameObject.FindObjectOfType<FireCooldownMeter>();
        frostCooldownMeter = GameObject.FindObjectOfType<FrostCooldownMeter>();
        earthCooldownMeter = GameObject.FindObjectOfType<EarthCooldownMeter>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > fireCooldownDuration)
        {
            if (Input.GetKeyDown(fireballKey) || Input.GetButtonDown("Fire") && canUseFire)
            {
                Shoot(fireballKey);
                fireCooldownDuration = Time.time + fireCooldown;
                fireCooldownMeter.FlameResetMeter();
            }
        }

        fireCooldownMeter.FlameHandleCooldown(fireCooldown);

        if (Time.time > frostCooldownDuration)
        {
            if (Input.GetKeyDown(frostballKey) || Input.GetButtonDown("Ice") && canUseFrost)
            {
                Shoot(frostballKey);
                frostCooldownDuration = Time.time + frostCooldown;
                frostCooldownMeter.FrostResetMeter();
            }
        }

        frostCooldownMeter.FrostHandleCooldown(frostCooldown);

        if(Time.time > earthCooldownDuration)
        {
            if (Input.GetKeyDown(earthSpellKey) || Input.GetButtonDown("Rock") && canUseEarth)
            {
                Shoot(earthSpellKey);
                earthCooldownDuration = Time.time + earthCooldown;
                earthCooldownMeter.EarthResetMeter();
            }
        }

        earthCooldownMeter.EarthHandleCooldown(earthCooldown);
    }

    void Shoot(string key)
    {
        if (key == fireballKey)
        {
            fireSource.Play();
            Instantiate(fireballprefab, fireSpot.position, fireSpot.rotation);
        }
        if (key == frostballKey)
        {
            Instantiate(frostballprefab, fireSpot.position, fireSpot.rotation);
        }
        if(key == earthSpellKey)
        {
            earthSource.Play();
            Instantiate(earthSpellprefab, new Vector3(fireSpot.position.x, fireSpot.position.y - 0.97f, fireSpot.position.z), fireSpot.rotation);
        }
    }
}
