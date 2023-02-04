using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField]
    private int currentHealth;
    public int CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            currentHealth = value;
            if (currentHealth <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }

    [SerializeField]
    private int maxHealth;
    public int MaxHealth
    {
        get { return maxHealth; }
        set
        {
            maxHealth = value;
            currentHealth = value;
        }
    }

    [SerializeField]
    private float speed;
    public float Speed
    {
        get { return speed; }
        set
        {
            speed = value;
        }
    }

    [SerializeField]
    private float damage;
    public float Damage
    {
        get { return damage; }
        set
        {
            damage = value;
        }
    }
}
