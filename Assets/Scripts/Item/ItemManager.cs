using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    private List<ItemSO> items;
    private readonly Dictionary<ItemType, string> itemTypeToString = new();

    private ObjectPool pool;

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
        pool = ObjectPool.Instance;
        InitItemTypeString();

        usedItemCount = PlayerPrefs.GetInt("ItemScore", 0);
    }

    private void InitItemTypeString()
    {
        for(int i = 0; i < Enum.GetValues(typeof(ItemType)).Length; i ++)
        {
            string type = ((ItemType)i).ToString();
            string firstCase = type[0].ToString();
            type = type[1..].ToLower();
            type = string.Concat(firstCase, type);

            itemTypeToString.Add((ItemType)i, type);
        }
    }
    private string ItemTypeToString(ItemType type) => itemTypeToString[type];

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

        GameObject item = pool.SpawnFromPool(type);
        item.GetComponent<ItemBase>().Init(itemInfo);

        return item;
    }

    public void ItemUse()
    {
        usedItemCount++;
        AchievementManager.Instance.CheckAchievement(AchievementType.ITEM, usedItemCount);
        PlayerPrefs.SetInt("ItemScore", usedItemCount);
        PlayerPrefs.Save();
    }
}
