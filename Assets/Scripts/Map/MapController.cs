using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public float speed;

    public int sccelerationCount;

    public float Break(float inSpeed, float inMinSpeed)
    {
        // TODO :: 플레이어가 데미지를 입을시 최소 속도로 변경
        if (inSpeed <= inMinSpeed)
        {
            return speed = inMinSpeed;
        }
        return inSpeed;
    }

    public float acceleration(float inSpeed, float inMaxSpeed)
    {
        if (inSpeed >= inMaxSpeed)
        {
            return speed = inMaxSpeed;
        }
        return inSpeed;
    }
}
