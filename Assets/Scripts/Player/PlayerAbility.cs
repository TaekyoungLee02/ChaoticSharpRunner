using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbility : MonoBehaviour
{
    public event Action<float, float> OnAbilityGaugeChanged;
    public event Action OnSlowDown;
    public event Action OnRestoreSpeed;

    [SerializeField] private float currentGauge;
    [SerializeField] private float maxGauge;
    public float MaxGauge => maxGauge;

    [SerializeField] private float gaugeRechargeRate;
    [SerializeField] private float gaugeDrainRate;

    private bool isAbilityActive = false;
    private Coroutine drainCoroutine;

    void Start()
    {
        currentGauge = maxGauge;
        UpdateAbilityGaugeUI();
    }

    void Update()
    {
        if (!isAbilityActive)
        {
            RechargeGauge();
        }
    }

    public void OnAbilityUse(InputAction.CallbackContext context)
    {
        if (context.performed && currentGauge > 0)
        {
            StartAbility();
        }

        else if (context.canceled)
        {
            EndAbility();
        }
    }

    private void StartAbility()
    {
        if (!isAbilityActive && currentGauge > 0)
        {
            isAbilityActive = true;
            OnSlowDown?.Invoke();
            drainCoroutine = StartCoroutine(DrainGauge());
        }
    }

    private void EndAbility()
    {
        if (isAbilityActive)
        {
            isAbilityActive = false;
            OnRestoreSpeed?.Invoke();

            if (drainCoroutine != null)
            {
                StopCoroutine(drainCoroutine);
                drainCoroutine = null;
            }
        }
    }

    private void RechargeGauge()
    {
        if (currentGauge < maxGauge)
        {
            currentGauge += gaugeRechargeRate * Time.deltaTime;
            UpdateAbilityGaugeUI();
        }
    }

    private IEnumerator DrainGauge()
    {
        while (isAbilityActive && currentGauge > 0)
        {
            currentGauge -= gaugeDrainRate * Time.deltaTime;
            UpdateAbilityGaugeUI();

            if (currentGauge <= 0)
            {
                EndAbility();
            }

            yield return null;
        }
    }

    private void UpdateAbilityGaugeUI()
    {
        OnAbilityGaugeChanged?.Invoke(currentGauge, maxGauge);
    }

    public void InitializeAbility()
    {
        isAbilityActive = false;
        currentGauge = maxGauge;
        UpdateAbilityGaugeUI();
        OnRestoreSpeed?.Invoke();

        if (drainCoroutine != null)
        {
            StopCoroutine(drainCoroutine);
            drainCoroutine = null;
        }
    }
}