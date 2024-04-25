using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthComponent : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    //public int bossHealth = 300;
    //public int bossCurrentHealth;

    public Image healthBar;
    //public Image bossHealthBar;

    [SerializeField] private bool isBoss;
    [SerializeField] private AudioClip damageSound;

    // Start is called before the first frame update
    void Start()
    {
        
        currentHealth = maxHealth;
        
    }

    
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthBar.fillAmount = currentHealth / 100f;
            
        AudioManager.instance.PlaySoundClip(damageSound, 20);
        


    }
    
    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public float GetHealth()
    {
        return currentHealth;
    }
}
