using UnityEngine;

public class MapController : MonoBehaviour
{
    public float speed;
    
    private float minSpeed;
    public float maxSpeed;
    private float saveSpeed;

    private float ranTime;
    
    public float accelerationCoolTime;

    // TODO :: PlayerAbility
    public bool inPlayerDamage = false;
    public bool inPlayeritem = false;

    private void Start()
    {
        minSpeed = speed;
        
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
            // �� ���ӵ� ��ġ�� ������ ����
        }
        else if (inEnvironment == "Day")
        {
            accelerationCoolTime = 5;
            // ������ �� ���ӵ��� ���� ��ġ�� ����
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
}
