using UnityEngine;

[CreateAssetMenu(fileName ="Item", menuName = "Item")]
public class ItemSO : ScriptableObject
{
    [Header("Basic Information")]
    public string itemName;
    public string itemDescription;
    public ItemType itemType;

    [Header("Item Value")]
    public float itemValue;
    public float duration;
}

public enum ItemType
{
    COIN,
    HEAL,
    MAGNET,
    CHANGECOIN,
    SUPERARMOR
}
