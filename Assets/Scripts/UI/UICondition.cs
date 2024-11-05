using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UICondition : MonoBehaviour
{
    [SerializeField] private Image healthUIBar;
    [SerializeField] private List<GameObject> lifeIcons;
    [SerializeField] private Image abilityGaugeUIBar;

    private PlayerStats playerStats;
    private PlayerAbility playerAbility;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        playerStats = GameManager.Instance.player.stats;
        playerAbility = GameManager.Instance.player.ability;

        if (playerStats != null)
        {
            playerStats.OnHealthChanged += UpdateHealthUI;
            playerStats.OnLifeChanged += UpdateLifeUI;
        }

        if (playerAbility != null)
        {
            playerAbility.OnAbilityGaugeChanged += UpdateAbilityGaugeUI;
        }

        ResetUI();
    }

    public void ResetUI()
    {
        if (playerStats != null)
        {
            UpdateHealthUI(playerStats.MaxHealth);
            UpdateLifeUI(playerStats.MaxLife);
        }

        if (playerAbility != null)
        {
            UpdateAbilityGaugeUI(playerAbility.MaxGauge, playerAbility.MaxGauge);
        }
    }

    private void UpdateHealthUI(int health)
    {
        if (healthUIBar != null)
            healthUIBar.fillAmount = (float)health / playerStats.MaxHealth;
    }

    private void UpdateLifeUI(int life)
    {
        for (int i = 0; i < lifeIcons.Count; i++)
        {
            lifeIcons[i].SetActive(i < life);
        }
    }

    private void UpdateAbilityGaugeUI(float currentGauge, float maxGauge)
    {
        if (abilityGaugeUIBar != null)
            abilityGaugeUIBar.fillAmount = currentGauge / maxGauge;
    }
}