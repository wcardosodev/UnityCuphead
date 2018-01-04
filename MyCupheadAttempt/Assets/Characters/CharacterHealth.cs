using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour {

    [SerializeField] int maximumHealth;
    int currentHealth;

	void Start () {
        currentHealth = maximumHealth;
	}

    public void TakeDamage(int damage = 1)
    {
        //If player, damage cannot surpass 1
        if(gameObject.tag == "Player")
        {
            damage = Mathf.Clamp(damage, 1, 1);
        }
        currentHealth -= damage;
        //play hit sound
        //play animation flash on hit
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //Play Animation
        //Play Sound
        //Remove collider
        print("Dead");
        Destroy(gameObject);
    }

    public float HealthAsPercent
    {
        get { return ((float)currentHealth / maximumHealth); }
    }

    public float Health
    {
        get { return currentHealth; }
    }
}
