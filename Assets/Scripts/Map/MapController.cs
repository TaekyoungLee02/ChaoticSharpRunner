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

        // �̺�Ʈ ���: ȯ���� �ٲ� �� UpdateObstacleBehavior ȣ��
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
                // �� ���ӵ� ��Ÿ���� ������ ����
            }
            else if (inEnvironment == "Day")
            {
                accelerationCoolTime = 5;
                // ������ �� ���ӵ� ��Ÿ���� ���� ��ġ�� ����
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
        { // ����� �ӵ��� ���ٸ�
            saveSpeed = speed;
            speed = minSpeed;
        }

        if (isPlayerDamage)
        { // �������� �Դ´ٸ� // �̰͵� �ʿ���ڳ�
            runTime = 0f;
            saveSpeed = minSpeed;
            speed = minSpeed;
        }
        // bool�� �ʿ���� �������� �ڵ� ���ƿ��� �ڵ�
        // �������� �͵� ��������
    }

    private void AccelerationSpeed()
    {
        if (saveSpeed != 0)
        { // ������ ȿ���� ��Ȱ��ȭ ������ �� ����� �ӵ��� �ִٸ�
            speed = saveSpeed;
            saveSpeed = 0;
            // �ӵ��� ����� ������ ���� �� �ʱ�ȭ
        }

        if (runTime >= accelerationCoolTime)
        { // ���� ��Ÿ���� �Ѿ��ٸ�
            speed++;
            runTime = 0f;
            // ���� �� �ʱ�ȭ
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
