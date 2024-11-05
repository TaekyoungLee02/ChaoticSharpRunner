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

        // �̺�Ʈ ���: ȯ���� �ٲ� �� UpdateObstacleBehavior ȣ��
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
            // �� ���ӵ� ��Ÿ���� ������ ����
        }
        else if (inEnvironment == "Day")
        {
            accelerationCoolTime = 5;
            // ������ �� ���ӵ� ��Ÿ���� ���� ��ġ�� ����
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
        { // ������ ȿ���� Ȱ��ȭ ������ �� ����� �ӵ��� ���ٸ�
            saveSpeed = speed;
            speed = minSpeed;
        }

        if (inPlayerDamage)
        { // ������ ȿ���� ���� �� �������� �Դ´ٸ�
            ranTime = 0f;
            saveSpeed = minSpeed;
            speed = minSpeed;
        }
    }

    private void AccelerationSpeed()
    {
        if (!inPlayeritem && saveSpeed != 0)
        { // ������ ȿ���� ��Ȱ��ȭ ������ �� ����� �ӵ��� �ִٸ�
            speed = saveSpeed;
            saveSpeed = 0;
            // �ӵ��� ����� ������ ���� �� �ʱ�ȭ
        }

        if (ranTime >= accelerationCoolTime)
        { // ���� ��Ÿ���� �Ѿ��ٸ�
            speed++;
            ranTime = 0f;
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
        ranTime = 0f;
        accelerationCoolTime = 5f;
        inPlayerDamage = false;
        inPlayeritem = false;
    }
}
