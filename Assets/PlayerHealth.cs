using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int playerHealth;
    public GameObject healthBar;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = 6;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            DamagePlayer(1);
        }
    }
    public int GetPlayerHealth()
    {
        return playerHealth;
    }

    public void DamagePlayer(int damage)
    {
        playerHealth -= damage;
        if(playerHealth <= 0)
        {
            Die();
        }
        else
        {
            healthBar.GetComponent<HealthUpdater>().UpdateUIPlayerHealth(playerHealth);
        }
    }
    public void AddHealthToPlayer(int health)
    {
        playerHealth += health;
        if(playerHealth > 6)
        {
            playerHealth = 6;
        }
        healthBar.GetComponent<HealthUpdater>().UpdateUIPlayerHealth(playerHealth);
    }

    public void Die()
    {
        //change scene to death scene
    }

}
