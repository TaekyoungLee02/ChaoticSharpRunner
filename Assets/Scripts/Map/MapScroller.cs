using UnityEngine;

public class MapScroller : MonoBehaviour
{
    public float speed;

    private float minSpeed;
    private float maxSpeed;

    private Vector3 targetPosition;
    private Vector3 resetPosition;

    public ObjectPool pool;
    public SpawnController controller;

    private void Awake()
    {
        pool = GetComponentInParent<ObjectPool>();
        controller = GetComponentInParent<SpawnController>();
    }

    private void Start()
    {
        minSpeed = speed;
        maxSpeed = 40f;
        targetPosition = new Vector3(0, 0, -30f);
        resetPosition = new Vector3(0, 0, 90);
    }

    private void OnEnable()
    {
        
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            controller.GetMapObject(pool).transform.position = resetPosition;
            gameObject.SetActive(false);
        }
        else
        {
            transform.position = Vector3.MoveTowards
                (transform.position, targetPosition, speed * Time.fixedDeltaTime);
        }
    }
}
