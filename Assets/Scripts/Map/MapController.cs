using System.Collections;
using System.Collections.Generic;
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

    public float MapSpeed()
    {
        ResetSpeed();
        AccelerationSpeed();

        return speed;
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

        LimitSpeed();
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
            speed += 1;
            ranTime = 0f;
            // ���� �� �ʱ�ȭ
        }

        LimitSpeed();
    }

    private void LimitSpeed()
    {
        if (speed <= minSpeed)
        { // �ӵ� �ּ�ġ
            speed = minSpeed;
        }
        else if (speed >= maxSpeed)
        { // �ӵ� �ִ�ġ
            speed = maxSpeed;
        }
    }
}
