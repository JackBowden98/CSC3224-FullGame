using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    public DevTools devTools;
    public static PlayerCombatController instance;

    // if combat is enabled
    [SerializeField] private bool combatEnabled;
    // time you can apply an input and attack damage
    [SerializeField] private float inputTimer, attackDamage;

    // fields required for circle cast for damage
    [SerializeField] private Transform m_HitCheck;
    [SerializeField] private float k_HitRadius;
    [SerializeField] private LayerMask m_WhatIsDamagable;

    // if there is an iput and is attacking
    private bool gotInput;
    public bool isAttacking;
    public bool isDashing;

    // the parameters passed to the object being damaged
    private float[] attackDetails = new float[2];
    HitPause hitPause;

    [SerializeField] private float maxHealth;
    private float currentHealth;

    bool invincible;


    // time of the last input
    private float lastInputTime = Mathf.NegativeInfinity;

    private Animator anim;

    private CharacterController2D cc;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        

        anim = GetComponent<Animator>();
        anim.SetBool("CanAttack", combatEnabled);
        cc = GetComponent<CharacterController2D>();
        hitPause = GetComponent <HitPause>();
        currentHealth = maxHealth;
        invincible = false;
    }

    private void Update()
    {
        CheckCombatInput();
        checkAttacks();

        if (Input.GetKeyDown("o") && devTools.toolsShown)
        {
            ToggleInvincible();
        }
    }

    // checks the input and enables combat
    private void CheckCombatInput()
    {
        if (Input.GetButtonDown("Attack"))
        {
            if (combatEnabled)
            {
                gotInput = true;
                lastInputTime = Time.time;
            }
        }
    }

    // if there is an input and not already attacking, attack
    private void checkAttacks()
    {
        if (gotInput)
        {
            if (!isAttacking)
            {
                FindObjectOfType<AudioManager>().Play("Slash");
                gotInput = false;
                isAttacking = true;
                anim.SetBool("Attack", true);
                anim.SetBool("IsAttacking", isAttacking);
            }
        }

        if (Time.time >= lastInputTime + inputTimer)
        {
            gotInput = false;
        }
    }

    private void CheckAttackHitbox()
    {

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(m_HitCheck.position, k_HitRadius, m_WhatIsDamagable);
        attackDetails[0] = attackDamage;
        attackDetails[1] = transform.position.x;
        foreach (Collider2D collider in hitColliders)
        {
            Debug.Log("Hit!");
            collider.SendMessage("Damage", attackDetails);
        }
        int direction = cc.GetFacingDirection();
        cc.Recoil(-direction);
    }

    private void FinishAttack()
    {
        isAttacking = false;
        anim.SetBool("IsAttacking", isAttacking);
        anim.SetBool("Attack", false);
    }

    private void Damage(float[] attackDetails)
    {
        if (!invincible && !isDashing)
        {
            currentHealth -= attackDetails[0];
            int direction;
            hitPause.Pause();
            Health.instance.SetHealth(currentHealth);

            if (attackDetails[1] < transform.position.x)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }
            System.Console.WriteLine(currentHealth);
            cc.KnockBack(direction);

            if (currentHealth <= 0.0f)
            {
                string level = "Death";
                // go to complete screen
                Application.LoadLevel(level);
            }
        }
    }

    public void ToggleInvincible()
    {
        invincible = !invincible;
    }

    public void Heal(float healValue)
    {
        currentHealth += healValue;

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }

        Health.instance.SetHealth(currentHealth);
    }

    public float getMaxHealth()
    {
        return maxHealth;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(m_HitCheck.position, k_HitRadius);
    }
}
