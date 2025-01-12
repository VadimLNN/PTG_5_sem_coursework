using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Range(1, 100)]
    [SerializeField] int maxHealth;
    [Range(0, 100)]
    [SerializeField] float currentHealth;

    public UnityEvent<int, int> onHealthChange;

    public UnityEvent <Vector3, string> spawnOnDeath;
    public UnityEvent onDeath;
    public UnityEvent onHitTaken;

    private void Start() => onHealthChange?.Invoke((int)currentHealth, maxHealth);
 
    public bool changeHealth(int amount)
    {
        if (currentHealth == maxHealth)
            return false;

        currentHealth += amount;

        if(currentHealth > maxHealth)
            currentHealth = maxHealth;

        if(currentHealth < 0)
            currentHealth = 0;

        onHealthChange?.Invoke((int)currentHealth, maxHealth);

        return true;
    }

    public void hpDecrease(float amount)
    {
        if (currentHealth <= 0) return;

        onHitTaken?.Invoke();

        currentHealth = Mathf.FloorToInt(currentHealth - amount);

        if(currentHealth < 0)
            currentHealth = 0;

        onHealthChange?.Invoke((int)currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            onDeath?.Invoke();
            
            string typeOfItem = "soul";

            if (Random.Range(0,10) > 2)
                switch (transform.name)
                {
                    case "Bat(Clone)":
                        typeOfItem = "wings";
                        break;
                    case "Golem(Clone)":
                        typeOfItem = "cristal";
                        break;
                    case "Mushroom(Clone)":
                        typeOfItem = "mushroom";
                        break;
                    default:
                        typeOfItem = "soul";
                        break;
                }

            spawnOnDeath?.Invoke(transform.position, typeOfItem);
        }
    }
}
