using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class MapScroller : MonoBehaviour
{
    private Vector3 targetPosition;

    private GameObject spawnPositionObject;

    private SpawnController spawnController;
    private MapController mapController;
    [SerializeField]
    public Transform[] coinSpawnPosition;
    [SerializeField]
    public Transform[] trapSpawnPosition;
    [SerializeField]
    public Transform[] itemSpawnPosition;

    public List<Transform> mapAttachedObjects = new();

    private void Awake()
    {
        spawnController = GetComponentInParent<SpawnController>();
        mapController = GetComponentInParent<MapController>();
    }

    private void Start()
    {
        targetPosition = new Vector3(0, -1, -30f);
    }

    private void OnEnable()
    {
        if (spawnController.mapObjectArray.Count > 0)
        {
            spawnPositionObject = spawnController.mapObjectArray
                [spawnController.mapObjectArray.Count - 1];

            transform.position =
                spawnPositionObject.transform.position + new Vector3(0, -1, 30f);
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
            GetMapObject();
        spawnController.mapObjectArray.Add(newMapObject);

        foreach(var obj in mapAttachedObjects)
        {
            obj.SetParent(null);
            obj.transform.position = new(0, -100, 0);
            obj.gameObject.SetActive(false);
        }

        mapAttachedObjects.Clear();
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
