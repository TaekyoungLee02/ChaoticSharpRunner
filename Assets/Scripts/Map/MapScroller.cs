using UnityEngine;

public class MapScroller : MonoBehaviour
{
    public int key;

    public float speed;

    //private float minSpeed;
    //private float maxSpeed;

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
        //minSpeed = speed;
        //maxSpeed = 40f;
        targetPosition = new Vector3(0, 0, -29.9f);
        resetPosition = new Vector3(0, 0, 90);
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            int randomMap = Random.Range((int)ObjectType.Map, (int)ObjectType.Map + (int)MapType.Max);
            GameObject newMap = pool.SpawnFromPool(randomMap);
            newMap.transform.position = resetPosition;
            gameObject.SetActive(false);
        }
        else
        {
            transform.position = Vector3.MoveTowards
                (transform.position, targetPosition, speed * Time.fixedDeltaTime);
        }
    }
}
