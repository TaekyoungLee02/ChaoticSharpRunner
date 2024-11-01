using UnityEngine;

public class MapScroller : MonoBehaviour
{
    private Vector3 targetPosition;
    private Vector3 resetPosition;

    public ObjectPool pool;
    public SpawnController controller;
    private MapController mapController;

    private void Awake()
    {
        pool = GetComponentInParent<ObjectPool>();
        controller = GetComponentInParent<SpawnController>();
        mapController = GetComponentInParent<MapController>();
    }

    private void Start()
    {
        targetPosition = new Vector3(0, 0, 0);
        resetPosition = new Vector3(0, 0, 89f);
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, targetPosition) <= 0.01f)
        {
            controller.GetMapObject(pool).transform.position = resetPosition;
            gameObject.SetActive(false);
        }
        else
        {
            transform.position = Vector3.MoveTowards
                (transform.position, targetPosition, mapController.MapSpeed()
                * Time.fixedDeltaTime);
        }
    }
}
