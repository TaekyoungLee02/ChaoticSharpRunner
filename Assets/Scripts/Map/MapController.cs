using UnityEngine;

public class MapController : MonoBehaviour
{
    private float speed;
    private float minSpeed;
    private float maxSpeed;
    private float saveSpeed;

    private float ranTime;
    
    public float accelerationCoolTime;

    // TODO :: PlayerAbility
    public bool inPlayerDamage = false;
    public bool inPlayeritem = false;

    private void Start()
    {
        speed = 5f;
        minSpeed = speed;
        maxSpeed = 30f;
        saveSpeed = 0f;
        ranTime = 0f;

        // 이벤트 등록: 환경이 바뀔 때 UpdateObstacleBehavior 호출
        EnvironmentManager.Instance.OnEnvironmentChanged += UpdateMapBehavior;

        //GameManager.Instance.player.ability.OnSlowDown += ResetSpeed;
        //GameManager.Instance.player.ability.OnRestoreSpeed += AccelerationSpeed;
    }

    private void FixedUpdate()
    {
        if (!inPlayeritem)
        {
            ranTime += Time.fixedDeltaTime;
        }
    }

    private void UpdateMapBehavior(string inEnvironment)
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

    public float MapSpeed()
    {
        ResetSpeed();
        AccelerationSpeed();

        return LimitSpeed();
    }

    private void ResetSpeed()
    {
        if (inPlayeritem && saveSpeed == 0)
        { // 아이템 효과가 활성화 상태일 때 저장된 속도가 없다면
            saveSpeed = speed;
            speed = minSpeed;
        }

        if (inPlayerDamage)
        { // 아이템 효과가 없을 때 데미지를 입는다면
            ranTime = 0f;
            saveSpeed = minSpeed;
            speed = minSpeed;
        }
    }

    private void AccelerationSpeed()
    {
        if (!inPlayeritem && saveSpeed != 0)
        { // 아이템 효과가 비활성화 상태일 때 저장된 속도가 있다면
            speed = saveSpeed;
            saveSpeed = 0;
            // 속도를 저장된 값으로 설정 후 초기화
        }

        if (ranTime >= accelerationCoolTime)
        { // 가속 쿨타임을 넘었다면
            speed++;
            ranTime = 0f;
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
        ranTime = 0f;
        accelerationCoolTime = 5f;
        inPlayerDamage = false;
        inPlayeritem = false;
    }
}
