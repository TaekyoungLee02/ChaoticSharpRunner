using UnityEngine;

[CreateAssetMenu(fileName = "MapSpawnSettings", menuName = "Settings/MapSpawnSetting")]
public class MapSpawnSetting : ScriptableObject
{
    public string[] mapNameArray;
    public float mapSpawnDistance;
    public int mapSpawnCount;
}