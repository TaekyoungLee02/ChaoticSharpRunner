using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class AccessoryManager
{
    private static AccessoryDataSO[] m_AccessoryList;

    public static AccessoryDataSO[] GetList ()
    {
        if (m_AccessoryList == null)
            Load();
        return m_AccessoryList;
    }

    public static List<AccessoryDataSO> GetListBySlot(SlotLocation slot)
    {
        var list = GetList();
        return list.Where(i => i.slot == slot).ToList();
    }

    private static void Load ()
    {
        m_AccessoryList = Resources.LoadAll<AccessoryDataSO>("Data/Accessory");
    }


}
