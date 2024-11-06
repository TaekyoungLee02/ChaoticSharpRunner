using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private float minSpeed;
    private float maxSpeed;
    private float saveSpeed;

    [SerializeField]
    private float runTime;
    [SerializeField]
    private float accelerationCoolTime;

    // TODO :: Player
    public bool isPlayerDamage = false;

    private void Start()
    {
        GameManager.Instance.OnGameReset += InitializeMapData;
        GameManager.Instance.OnGameRestart += InitializeMapData;
        GameManager.Instance.OnGameStart += InitializeMapData;

        GameManager.Instance.OnSpeedStart += StartSpeed;

        speed = 0f;
        minSpeed = speed;
        maxSpeed = 30f;
        saveSpeed = 0f;
        runTime = 0f;
        accelerationCoolTime = 1000f;

        // 이벤트 등록: 환경이 바뀔 때 UpdateObstacleBehavior 호출
        EnvironmentManager.Instance.OnEnvironmentChanged += UpdateMapBehavior;

        GameManager.Instance.player.ability.OnSlowDown += SlowSpeed;
        GameManager.Instance.player.ability.OnRestoreSpeed += ResetSpeed;
    }

    private void FixedUpdate()
    {
        if (speed != 0)
        {
            runTime += Time.fixedDeltaTime;
        }
    }

    private void UpdateMapBehavior(string inEnvironment)
    {
        if (speed != 0)
        {
            if (inEnvironment == "Night")
            {
                accelerationCoolTime = 3;
                // 맵 가속도 쿨타임을 빠르게 변경
            }
            else if (inEnvironment == "Day")
            {
                accelerationCoolTime = 5;
                // 낮에는 맵 가속도 쿨타임을 본래 수치로 변경
            }
        }
    }

    public void StartSpeed()
    {
        speed = 5f;
        minSpeed = speed;
        maxSpeed = 30f;
        saveSpeed = 0f;
        runTime = 0f;
        accelerationCoolTime = 5f;
    }

    public float MapSpeed()
    {
        AccelerationSpeed();

        return LimitSpeed();
    }

    private void SlowSpeed()
    {
        saveSpeed = speed;
        speed *= 0.5f;
    }

    private void ResetSpeed()
    {
        if (saveSpeed != 0)
        {
            speed = saveSpeed;
            saveSpeed = 0;
        }
    }

    private void AccelerationSpeed()
    {
        if (runTime >= accelerationCoolTime)
        { // 가속 쿨타임을 넘었다면
            speed++;
            runTime = 0f;
            // 가속 후 초기화
        }
    }

    private float LimitSpeed()
    {
        return speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
    }

    public void InitializeMapData()
    {
        speed = 5f;
        saveSpeed = 0f;
        runTime = 0f;
        accelerationCoolTime = 5f;
        isPlayerDamage = false;
    }
}
