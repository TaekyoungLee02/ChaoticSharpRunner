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

        GameManager.Instance.player.ability.OnSlowDown += ResetSpeed;
        GameManager.Instance.player.ability.OnRestoreSpeed += AccelerationSpeed;
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
        ResetSpeed();
        AccelerationSpeed();

        return LimitSpeed();
    }

    private void ResetSpeed()
    {
        if (saveSpeed == 0)
        { // 저장된 속도가 없다면
            saveSpeed = speed;
            speed = minSpeed;
        }

        if (isPlayerDamage)
        { // 데미지를 입는다면 // 이것도 필요없겠네
            runTime = 0f;
            saveSpeed = minSpeed;
            speed = minSpeed;
        }
        // bool값 필요없이 느려지는 코드 돌아오는 코드
        // 빨라지는 것도 마찬가지
    }

    private void AccelerationSpeed()
    {
        if (saveSpeed != 0)
        { // 아이템 효과가 비활성화 상태일 때 저장된 속도가 있다면
            speed = saveSpeed;
            saveSpeed = 0;
            // 속도를 저장된 값으로 설정 후 초기화
        }

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
