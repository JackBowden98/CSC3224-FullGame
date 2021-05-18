using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MushersHealth : MonoBehaviour
{
    public static MushersHealth instance;
    public Image healthBar;

    [SerializeField] private Color maxColour;
    [SerializeField] private Color minColour;

    public float health;
    [SerializeField] private float maxHealth;
    float lerpSpeed;

    void Start()
    {
        health = maxHealth;
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {

        lerpSpeed = 3f * Time.deltaTime;

        StaminaBarFiller();
        ColorChanger();
    }

    void StaminaBarFiller()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, (health / maxHealth), lerpSpeed);

    }
    void ColorChanger()
    {
        Color StaminaColour = Color.Lerp(minColour, maxColour, (health / maxHealth));
        healthBar.color = StaminaColour;
    }

    public void Heal(float healingPoints)
    {
        if (health < maxHealth)
            health += healingPoints;
    }
    public void Damage(float damagePoints)
    {
        if (health > 0)
            health -= damagePoints;
    }

    public void SetHealth(float health)
    {
        this.health = health;
    }
}
