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

    [SerializeField] private PlayerBlink playerBlink;

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        health = Mathf.Clamp(health - damage, 0, maxHealth);

        OnHealthChanged?.Invoke(health);

        // �´� �Ҹ�
        AudioManager.Instance.PlaySoundFXClip(AudioClipName.Sfx_Hit, transform.position, 0.5f);

        // ���� �Ҹ�
        int voiceRand = UnityEngine.Random.Range(0, 5);
        AudioManager.Instance.PlaySoundFXClip(AudioClipName.UnityChan_Aha1 + voiceRand, transform.position, 0.5f);


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

    public void ResetStats() // �װ� ����� �������� �ʿ��� �Լ�
    {
        life--;
        health = maxHealth;
        isInvincible = false;
        isSuperArmor = false;

        OnHealthChanged?.Invoke(health);
        OnLifeChanged?.Invoke(life);
        StartInvincibility(invincibilityDuration);
    }

    public void InitializeStats() // �ƿ� �ʱ�ȭ (���� �ٽý���, ó������ �Ҷ� �ʿ��� �Լ�)
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
            isInvincible = true;
            StartCoroutine(InvincibilityCoroutine(duration));
            playerBlink?.StartBlinking(duration);
        }
    }

    private IEnumerator InvincibilityCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        isInvincible = false;
    }

    public void StartSuperArmor(float duration)
    {
        if (!isSuperArmor)
        {
            isSuperArmor = true;
            StartCoroutine(SuperArmorCoroutine(duration));
        }
    }

    private IEnumerator SuperArmorCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        isSuperArmor = false;
    }
}