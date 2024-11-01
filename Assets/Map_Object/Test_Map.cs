using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Map : MonoBehaviour
{

    float speed = 20f;
    public Transform target;
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
        //targetPosition = new Vector3(0, 0, 0);
        resetPosition = new Vector3(0, 0, 90f);
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, target.position) <= 0.01f)
        {
            controller.GetMapObject(pool).transform.position = resetPosition;
            gameObject.SetActive(false);
        }
        else
        {
            transform.position = Vector3.MoveTowards
                (transform.position, target.position, speed* Time.fixedDeltaTime);
        }
    }
}
