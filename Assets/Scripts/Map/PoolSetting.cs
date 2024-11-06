using UnityEngine;

[CreateAssetMenu(fileName = "PoolSettings", menuName = "Pooling/PoolSettings")]
public class PoolSetting : ScriptableObject
{
    public string tag;
    public GameObject prefab;
    public int size;
    public Transform spawnPosition;
}
