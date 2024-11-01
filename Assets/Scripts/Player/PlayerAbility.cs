using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbility : MonoBehaviour
{
    public event Action<float, float> OnGaugeChanged;
    public event Action OnSlowDown;
    public event Action OnRestoreSpeed;

    [SerializeField] private float maxGauge;
    [SerializeField] private float gaugeRechargeRate;
    [SerializeField] private float gaugeDrainRate;

    private float currentGauge;
    private bool isAbilityActive = false;
    private Coroutine drainCoroutine;

    void Start()
    {
        currentGauge = maxGauge;
        UpdateGaugeUI();
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
            UpdateGaugeUI();
        }
    }

    private IEnumerator DrainGauge()
    {
        while (isAbilityActive && currentGauge > 0)
        {
            currentGauge -= gaugeDrainRate * Time.deltaTime;
            UpdateGaugeUI();

            if (currentGauge <= 0)
            {
                EndAbility();
            }

            yield return null;
        }
    }

    private void UpdateGaugeUI()
    {
        OnGaugeChanged?.Invoke(currentGauge, maxGauge);
    }

    public void InitializeAbility()
    {
        isAbilityActive = false;
        currentGauge = maxGauge;
        UpdateGaugeUI();
        OnRestoreSpeed?.Invoke();

        if (drainCoroutine != null)
        {
            StopCoroutine(drainCoroutine);
            drainCoroutine = null;
        }
    }
}