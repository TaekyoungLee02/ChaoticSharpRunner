using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public float speed;

    public int sccelerationCount;

    public float Break(float inSpeed, float inMinSpeed)
    {
        // TODO :: �÷��̾ �������� ������ �ּ� �ӵ��� ����
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
