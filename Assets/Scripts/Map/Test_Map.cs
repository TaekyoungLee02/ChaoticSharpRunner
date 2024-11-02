using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Map : MonoBehaviour
{
    public Vector3 targetPosition;

    public GameObject spawnPositionObject;

    public SpawnController spawnController;
    private MapController mapController;

    private void Awake()
    {
        spawnController = GetComponentInParent<SpawnController>();
        mapController = GetComponentInParent<MapController>();
    }

    private void Start()
    {
        targetPosition = new Vector3(0, 0, 0);
    }

    private void OnEnable()
    {
        if (spawnController.mapObjectArray.Count > 0)
        {
            spawnPositionObject = spawnController.mapObjectArray
                [spawnController.mapObjectArray.Count - 1];

            transform.position =
                spawnPositionObject.transform.position + new Vector3(0, 0, 30f);
        }
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, targetPosition) <= 0f)
        {
            PositionReSet();
        }
    }

    private void FixedUpdate()
    {
        MapMovement();
    }

    private void PositionReSet()
    {
        GameObject newMapObject = spawnController.
            GetMapObject(ObjectPool.Instance);
        spawnController.mapObjectArray.Add(newMapObject);

        gameObject.SetActive(false);
        spawnController.mapObjectArray.Remove(gameObject);
    }

    private void MapMovement()
    {
        transform.position = Vector3.MoveTowards
            (transform.position, targetPosition, mapController.
            MapSpeed() * Time.fixedDeltaTime);
    }
}
