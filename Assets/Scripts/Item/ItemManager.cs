using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    private List<ItemSO> items;
    private Dictionary<ItemType, string> itemTypeToString;

    private int usedItemCount;
    public int UsedItemCount { get { return usedItemCount; } }

    public override void Awake()
    {
        base.Awake();

        items = new List<ItemSO>();

        var so = Resources.LoadAll("ItemSOs/");
        foreach (var item in so)
        {
            items.Add(item as ItemSO);
        }
    }

    private void Start()
    {
        usedItemCount = PlayerPrefs.GetInt("ItemScore", 0);
    }

    private void InitItemTypeString()
    {
        itemTypeToString = new();

        for (int i = 0; i < System.Enum.GetValues(typeof(ItemType)).Length; i ++)
        {
            string type = ((ItemType)i).ToString();
            string firstCase = type[0].ToString();
            type = type[1..].ToLower();
            type = string.Concat(firstCase, type);

            itemTypeToString.Add((ItemType)i, type);
        }
    }
    private string ItemTypeToString(ItemType type)
    {
        if (itemTypeToString == null) InitItemTypeString();

        return itemTypeToString[type];
    }

    public GameObject SpawnItemRand(ItemType type)
    {
        var itemInfos = from item in items
                        where item.itemType == type
                        select item;

        var itemArray = itemInfos.ToArray();

        int index = Random.Range(0, itemArray.Length);

        return SpawnItem(itemArray[index]);
    }
    public GameObject SpawnItem(ItemType type, int index)
    {
        var itemInfos = from item in items
                        where item.itemType == type
                        select item;

        var itemArray = itemInfos.ToArray();

        index = Mathf.Clamp(index, 0, itemArray.Length - 1);

        return SpawnItem(itemArray[index]);
    }
    private GameObject SpawnItem(ItemSO itemInfo)
    {
        string type = ItemTypeToString(itemInfo.itemType);

        GameObject item = ObjectPool.Instance.SpawnFromPool(type);
        item.GetComponent<ItemBase>().Init(itemInfo);

        return item;
    }

    public void SpawnItemInMap(MapScroller map)
    {
        // Coin
        foreach(var coinT in map.coinSpawnPosition)
        {
            var coin = SpawnItemRand(ItemType.COIN);
            map.mapAttachedObjects.Add(coin.transform);

            coin.transform.position = coinT.position;
            coin.transform.SetParent(coinT);
        }

        // Item
        foreach (var itemT in map.itemSpawnPosition)
        {
            int randItem = Random.Range(1, System.Enum.GetValues(typeof(ItemType)).Length);

            var item = SpawnItemRand((ItemType)randItem);
            map.mapAttachedObjects.Add(item.transform);

            item.transform.position = itemT.position;
            item.transform.SetParent(itemT);
        }
    }

    public void ItemUse()
    {
        usedItemCount++;
        AchievementManager.Instance.CheckAchievement(AchievementType.ITEM, usedItemCount);
        PlayerPrefs.SetInt("ItemScore", usedItemCount);
        PlayerPrefs.Save();
    }
}
