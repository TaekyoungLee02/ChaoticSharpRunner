using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public float speed;

    public float ranTime;
    public float accelerationTime;

    public float minSpeed;
    public float maxSpeed;

    public bool playerDamage = false;

    private void Start()
    {
        minSpeed = speed;
    }

    private void Update()
    {
        ranTime += Time.deltaTime;
    }

    public float MapSpeed()
    {
        ResetSpeed();
        AccelerationSpeed();

        return speed;
    }

    public void ResetSpeed()
    {
        if (playerDamage)
        {
            ranTime = 0f;
            speed = minSpeed;
        }

        if (speed <= minSpeed)
        {
            speed = minSpeed;
        }
    }

    public void AccelerationSpeed()
    {
        if (ranTime >= accelerationTime)
        {
            speed += 0.01f;
            ranTime = 0f;
        }

        if (speed >= maxSpeed)
        {
            speed = maxSpeed;
        }
    }
}
