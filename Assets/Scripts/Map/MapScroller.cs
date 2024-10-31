using UnityEngine;

public class MapScroller : MonoBehaviour
{
    public float speed;

    private float minSpeed;
    private float maxSpeed;

    private Vector3 targetPosition;

    private void Start()
    {
        minSpeed = speed;
        maxSpeed = 40f;
        targetPosition = new Vector3(0, 0, -29.9f);
    }

    private void FixedUpdate()
    {
        BreakSpeed();

        if (transform.position.z <= targetPosition.z)
        {
            transform.position = new Vector3(0, 0, 90);
        }

        transform.position = Vector3.MoveTowards
            (transform.position, targetPosition, speed * Time.fixedDeltaTime);
    }

    public void BreakSpeed()
    {
        // ToDo :: 매개변수
        bool isPlayerDamage = false;

        if (isPlayerDamage)
        {
            speed = minSpeed;
        }
       
    }
}
