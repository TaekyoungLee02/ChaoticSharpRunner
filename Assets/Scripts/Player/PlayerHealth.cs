using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int life;

    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int maxLife = 3;

    public event Action<int> OnHealthChanged;
    public event Action<int> OnLifeChanged;
    public event Action OnPlayerDeath;

    public int GetHealth() => health;

    public int GetLife() => life;

    public void TakeDamage(int damage)
    {
        health = Mathf.Clamp(health - damage, 0, maxHealth);

        OnHealthChanged?.Invoke(health);

        if (health <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        health = Mathf.Clamp(health + amount, 0, maxHealth);

        OnHealthChanged?.Invoke(health);
    }

    void Die()
    {
        if (life > 0)
        {
            life--;

            ResetHealth();

            OnLifeChanged?.Invoke(life);
        }

        else
        {
            GameOver();
        }
    }

    void GameOver()
    {
        OnPlayerDeath?.Invoke();
    }

    public void ResetHealth()
    {
        health = maxHealth;

        OnHealthChanged?.Invoke(health);
    }

    public void InitializeHealth()
    {
        health = maxHealth;
        life = maxLife;

        OnHealthChanged?.Invoke(health);
        OnLifeChanged?.Invoke(life);
    }
}