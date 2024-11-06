using UnityEngine;
using UnityEngine.UI;

public class AbilityIconBlink : MonoBehaviour
{
    [SerializeField] private Image abilityIcon;
    [SerializeField] private float blinkInterval = 0.5f;
    private bool isBlinking = false;
    private float blinkTimer;

    void Start()
    {
        InitializeAbilityIcons();
    }

    private void InitializeAbilityIcons()
    {
        if (GameManager.Instance != null && GameManager.Instance.player != null && GameManager.Instance.player.ability != null)
        {
            GameManager.Instance.player.ability.OnSlowDown += StartBlinking;
            GameManager.Instance.player.ability.OnRestoreSpeed += StopBlinking;

            Color color = abilityIcon.color;
            color.a = 0f;
            abilityIcon.color = color;
        }
    }

    private void StartBlinking()
    {
        isBlinking = true;
        blinkTimer = 0f;
    }

    private void StopBlinking()
    {
        isBlinking = false;
        Color color = abilityIcon.color;
        color.a = 0f;
        abilityIcon.color = color;
    }

    void Update()
    {
        if (isBlinking)
        {
            blinkTimer += Time.deltaTime;
            if (blinkTimer >= blinkInterval)
            {
                blinkTimer = 0f;
                Color color = abilityIcon.color;
                color.a = color.a == 1f ? 0f : 1f;
                abilityIcon.color = color;
            }
        }
    }
}