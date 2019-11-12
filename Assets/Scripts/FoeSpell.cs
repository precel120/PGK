using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoeSpell : Spell
{
    Animator animator;
    public bool canFire;
    private float spellCooldownTimer = 0.0f;
    public float spellCooldown;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canFire && Time.time > spellCooldownTimer)
        {
            Fire();
            spellCooldownTimer = Time.time + spellCooldown;
        }
    }

    void Fire()
    {
        StartCoroutine(animateAttack());
    }

    private IEnumerator animateAttack()
    {
        animator.SetBool("IsAttacking", true);
        yield return new WaitForSeconds(0.6f);
        Instantiate(fireballprefab, fireSpot.position, fireSpot.rotation);
        yield return new WaitForSeconds(0.4f);
        animator.SetBool("IsAttacking", false);
    }
}
