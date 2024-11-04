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
    private float accelerationCoolTime;

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

    public void ResetSpeed()
    {
        if (inPlayeritem) // 조건 바꾸기
        { // 아이템 효과가 활성화 상태라면
            saveSpeed = speed;
        }

        if (inPlayerDamage)
        { // 아이템 효과가 없을 때 데미지를 입는다면
            ranTime = 0f;
            saveSpeed = minSpeed;
        }

        if (speed <= minSpeed)
        { // 속도 최소치
            speed = minSpeed;
        }
    }

    public void AccelerationSpeed()
    {
        if (!inPlayeritem && saveSpeed != 0)
        { // 아이템 효과가 없는데 저장된 속도가 존재한다면
            speed = saveSpeed;
            saveSpeed = 0;
            // 속도를 저장된 값으로 설정 후 초기화
        }

        if (ranTime >= accelerationCoolTime)
        { // 가속 쿨타임을 넘었다면
            speed += 1;
            ranTime = 0f;
            // 가속 후 초기화
        }

        if (speed >= maxSpeed)
        { // 속도 최대치
            speed = maxSpeed;
        }
    }
}
