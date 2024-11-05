using UnityEngine;

public class MapScroller : MonoBehaviour
{
    private Vector3 targetPosition;

    private GameObject spawnPositionObject;

    private SpawnController spawnController;
    private MapController mapController;
    [SerializeField]
    private Transform[] coinSpawnPosition;
    [SerializeField]
    private Transform[] trapSpawnPosition;
    [SerializeField]
    private Transform[] itemSpawnPosition;

    private void Awake()
    {
        spawnController = GetComponentInParent<SpawnController>();
        mapController = GetComponentInParent<MapController>();
    }

    private void Start()
    {
        targetPosition = new Vector3(0, 0, -30f);
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
            GetMapObject();
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
