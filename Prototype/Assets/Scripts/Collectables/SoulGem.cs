using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulGem : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float currentHealth;

    public GameObject player;
    private CharacterController2D cc;
    private SpriteRenderer sr;

    bool destroyable;
    int soulCount;
    public GameObject souls;

    HitPause hitPause;

    void Start()
    {
        cc = player.GetComponent<CharacterController2D>();
        sr = player.GetComponent<SpriteRenderer>();

        currentHealth = maxHealth;
        hitPause = GetComponent<HitPause>();
        destroyable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Damage(float[] attackDetails)
    {
        FindObjectOfType<AudioManager>().Play("GemHit");
        if (destroyable)
        {

            // 0 in the array is the amount of damage being recieved
            currentHealth -= attackDetails[0];
            FindObjectOfType<AudioManager>().Play("GemBreak");
            if (currentHealth <= 0.0f)
            {
                cc.dashEnabled = true;
                sr.color = new Color(0, 255, 0);
                Destroy(gameObject);
                //string level = "Complete";
                //LevelComplete(level);
            }
        }

    }

    /*
    public void LevelComplete(string level)
    {
        Application.LoadLevel(level);
    }
    */
    

    public void setDestroyable()
    {
        destroyable = true;
    }
}
