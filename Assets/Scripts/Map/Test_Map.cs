using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Map : MonoBehaviour
{
    float speed = 20f;

    public Vector3 targetPosition;
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
        resetPosition = new Vector3(0, 0, 89.6f);
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, targetPosition) <= 0f)
        {
            controller.GetMapObject(pool).transform.position = transform.position + resetPosition;
            gameObject.SetActive(false);
        }
        else
        {
            transform.position = Vector3.MoveTowards
                (transform.position, targetPosition, mapController.MapSpeed() * Time.fixedDeltaTime);
        }
    }
}
