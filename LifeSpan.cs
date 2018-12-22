using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeSpan : MonoBehaviour
{

    public float startHealth = 100.0f;
    private float health;

    public GameObject deathEffect;

    public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        health = startHealth;
    }

    // Update is called once per frame
    void Update()
    {
        lifeSpan();
    }

    void lifeSpan()
    {
        health -= 0.05f;

        healthBar.fillAmount = health / startHealth;

        if (health <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Destroy(gameObject);
    }
}
