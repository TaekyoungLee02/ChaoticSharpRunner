using System;
using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _life;

    public int health
    {
        get => _health;
        private set => _health = value;
    }

    public int life
    {
        get => _life;
        private set => _life = value;
    }

    public bool isInvincible { get; private set; }
    public bool isSuperArmor { get; private set; }

    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int maxLife = 3;
    [SerializeField] private float invincibilityDuration = 3;

    public int MaxHealth => maxHealth;
    public int MaxLife => maxLife;

    public event Action<int> OnHealthChanged;
    public event Action<int> OnLifeChanged;
    public event Action OnPlayerDeath;

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        health = Mathf.Clamp(health - damage, 0, maxHealth);

        OnHealthChanged?.Invoke(health);

        if (health <= 0)
        {
            Die();
        }

        else
        {
            StartInvincibility(invincibilityDuration);
        }
    }

    public void Heal(int amount)
    {
        health = Mathf.Clamp(health + amount, 0, maxHealth);

        OnHealthChanged?.Invoke(health);
    }

    private void Die()
    {
        if (life > 0)
        {
            GameManager.Instance.ResetGame();
        }

        else
        {
            GameManager.Instance.GameOver();
            OnPlayerDeath?.Invoke();
        }
    }

    public void ResetStats() // 죽고 목숨이 남았을때 필요한 함수
    {
        life--;
        health = maxHealth;
        isInvincible = false;
        isSuperArmor = false;

        OnHealthChanged?.Invoke(health);
        OnLifeChanged?.Invoke(life);
        StartInvincibility(invincibilityDuration);
    }

    public void InitializeStats() // 아예 초기화 (게임 다시시작, 처음시작 할때 필요한 함수)
    {
        health = maxHealth;
        life = maxLife;
        isInvincible = false;
        isSuperArmor = false;

        OnHealthChanged?.Invoke(health);
        OnLifeChanged?.Invoke(life);
    }

    public void StartInvincibility(float duration)
    {
        if (!isInvincible)
        {
            StartCoroutine(InvincibilityCoroutine(duration));
        }
    }

    private IEnumerator InvincibilityCoroutine(float duration)
    {
        isInvincible = true;
        yield return new WaitForSeconds(duration);
        isInvincible = false;
    }

    public void StartSuperArmor(float duration)
    {
        if (!isSuperArmor)
        {
            StartCoroutine(SuperArmorCoroutine(duration));
        }
    }

    private IEnumerator SuperArmorCoroutine(float duration)
    {
        isSuperArmor = true;
        yield return new WaitForSeconds(duration);
        isSuperArmor = false;
    }
}