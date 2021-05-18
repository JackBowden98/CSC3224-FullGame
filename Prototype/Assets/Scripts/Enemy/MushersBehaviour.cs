using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushersBehaviour : MonoBehaviour
{
    public Transform player;
    public bool isFlipped = false;
    private float lastApplyDamageTime;
    public float applyDamageCooldown;

    public Vector3 attackOffset;
    public float attackRange = 1f;
    public float attackDamage;

    [SerializeField] public GameObject HealthUI;

    public Transform attackPos;

    private Transform applyDamageCheck;
    private float[] attackDeatils = new float[2];

    public LayerMask whatIsPlayer;
    private Rigidbody2D rb;

    [SerializeField] private float maxHealth;
    public float currentHealth;

    HitPause hitPause;

    private int damageDirection;

    public void Start()
    {
        HealthUI.SetActive(false);
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        hitPause = GetComponent<HitPause>();
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x < player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x > player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    public void Attack()
    {
        if (Time.time > lastApplyDamageTime + applyDamageCooldown)
        {

            Collider2D hit = Physics2D.OverlapCircle(attackPos.position, attackRange, whatIsPlayer);

            if (hit != null)
            {
                Debug.Log("hey hey hey");
                lastApplyDamageTime = Time.time;
                attackDeatils[0] = attackDamage;
                attackDeatils[1] = rb.transform.position.x;
                hit.SendMessage("Damage", attackDeatils);
            }
        }
    }

    private void Damage(float[] attackDetails)
    {
        if (gameObject != null)
        {
            FindObjectOfType<AudioManager>().Play("SwordHitMusher");
            //hitPause.Pause();
            // 0 in the array is the amount of damage being recieved
            currentHealth -= attackDetails[0];
            MushersHealth.instance.SetHealth(currentHealth);

            // calculates whether player is on left or right side of the enemy
            if (attackDetails[1] > rb.transform.position.x)
            {
                damageDirection = -1;
            }
            else
            {
                damageDirection = 1;
            }

            // enemy is still alive
            if (currentHealth > 0.0f)
            {
                //hitPause.Pause();
            }
            else if (currentHealth <= 0.0f)
            {
                HealthUI.SetActive(false);
                //Destroy(gameObject);
                if ((PlayerPrefs.GetInt("HighScore") < CollectableManager.instance.souls))
                {
                    PlayerPrefs.SetInt("HighScore", CollectableManager.instance.souls);
                }
                 /*
                if ((PlayerPrefs.GetInt("BestMin") > DevTools.instance.minutes))
                {
                    if ((PlayerPrefs.GetInt("BestSec") > DevTools.instance.seconds))
                    {
                        PlayerPrefs.SetInt("BestMin", DevTools.instance.minutes);
                        PlayerPrefs.SetInt("BestSec", DevTools.instance.seconds);

                    }
                }
                 */
                Application.LoadLevel("Complete");
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
