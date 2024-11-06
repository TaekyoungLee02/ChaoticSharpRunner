using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnvironmentManager : Singleton<EnvironmentManager>
{
    public event Action<string> OnEnvironmentChanged;
    [SerializeField]
    private string currentEnvironment;
    [SerializeField]
    private string newEnvironment;

    [Range(0.0f, 1.0f)]
    public float time;
    public float fullDayLength;
    public float startTime;
    private float timeRate;
    public Vector3 noon; // 90, 0, 0

    [Header("Sun")]
    public Light sun;
    public Gradient sunColor;
    public AnimationCurve sunIntensity;

    [Header("Moon")]
    public Light moon;
    public Gradient moonColor;
    public AnimationCurve moonIntensity;

    [Header("Other Lighting")]
    public AnimationCurve lightingIntensityMultiplier;
    public AnimationCurve reflectionIntensityMultiplier;

    EnvironmentData EnvironmentSettingsData;

    private void Start()
    {
        if (EnvironmentSettingsData == null)
        {
            DataSetting(Resources.Load<EnvironmentData>("EnvironmentDatas/Environments"));
        }

        startTime = 0.4f;
        timeRate = 1.0f / fullDayLength;
        time = startTime;
        currentEnvironment = "Day";

        GameManager.Instance.OnGameReset += InitializeEnvironmentData;
        GameManager.Instance.OnGameRestart += InitializeEnvironmentData;
        GameManager.Instance.OnGameStart += InitializeEnvironmentData;
    }

    private void Update()
    {

        time = (time + timeRate * Time.deltaTime) % 1.0f;

        UpdateLighting(sun, sunColor, sunIntensity);
        UpdateLighting(moon, moonColor, moonIntensity);

        RenderSettings.ambientIntensity =
            lightingIntensityMultiplier.Evaluate(time);
        RenderSettings.reflectionIntensity =
            reflectionIntensityMultiplier.Evaluate(time);

        if (sun.gameObject.activeInHierarchy)
        {
            newEnvironment = "Day";
        }
        else
        {
            newEnvironment = "Night";
        }

        if (newEnvironment != currentEnvironment)
        { // 날씨가 변경되었다면 실행
            currentEnvironment = newEnvironment;
            OnEnvironmentChanged?.Invoke(currentEnvironment);
            // 환경 변경 이벤트 호출
        }
    }

    private void UpdateLighting(Light lightSource, Gradient colorGradiant,
        AnimationCurve intensityCurve)
    {
        float intensity = intensityCurve.Evaluate(time);

        lightSource.transform.eulerAngles =
            (time - (lightSource == sun ? 0.25f : 0.75f)) * noon * 4.0f;
        lightSource.color = colorGradiant.Evaluate(time);
        lightSource.intensity = intensity;

        GameObject lightObject = lightSource.gameObject;
        if (lightSource.intensity == 0 && lightObject.activeInHierarchy)
            lightObject.SetActive(false);
        else if (lightSource.intensity > 0 &&
            !lightObject.activeInHierarchy)
            lightObject.SetActive(true);
    }

    public void InitializeEnvironmentData()
    {
        timeRate = 1.0f / fullDayLength;
        time = startTime;
        currentEnvironment = "Day";
        newEnvironment = null;
    }

    private void DataSetting(EnvironmentData EnvironmentSettingsData)
    {
        fullDayLength = EnvironmentSettingsData.fullDayLength;
        noon = EnvironmentSettingsData.noon;

        sun = Instantiate(EnvironmentSettingsData.sun, transform);

        sunColor = EnvironmentSettingsData.sunColor;
        sunIntensity = EnvironmentSettingsData.sunIntensity;

        moon = Instantiate(EnvironmentSettingsData.moon, transform);
        moon.gameObject.SetActive(false);
        moonColor = EnvironmentSettingsData.moonColor;
        moonIntensity = EnvironmentSettingsData.moonIntensity;

        lightingIntensityMultiplier = EnvironmentSettingsData.lightingIntensityMultiplier;
        reflectionIntensityMultiplier = EnvironmentSettingsData.reflectionIntensityMultiplier;
        reflectionIntensityMultiplier = EnvironmentSettingsData.reflectionIntensityMultiplier;
    }
}