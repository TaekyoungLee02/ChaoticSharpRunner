using UnityEngine;

public class MapScroller : MonoBehaviour
{
    private Vector3 targetPosition;

    private GameObject spawnPositionObject;

    private SpawnController spawnController;
    private MapController mapController;

    public Transform[] mapSpawnPosition;

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

    private void OnDisable()
    {
        // 비활성화시 맵 재배치
        // 활성화시 재배치 된 오브젝트 활성화
        // 맵은 초기화 위치만 생성하고 맵 객체들을 오브젝트
        // 풀로 생성할까 고민중
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
