using System.Collections.Generic;
using UnityEngine;

public enum SlotLocation
{ 
    Head,
    Back,
}


public class UnityChanSlots : MonoBehaviour
{
    [SerializeField] private GameObject m_HeadSlot;
    [SerializeField] private GameObject m_BackSlot;

    private Dictionary<SlotLocation, GameObject> m_SocketDic;

    private void Awake()
    {
        m_SocketDic = new Dictionary<SlotLocation, GameObject>();
        m_SocketDic.Add(SlotLocation.Head, m_HeadSlot);
        m_SocketDic.Add(SlotLocation.Back, m_BackSlot);
    }

    public void EquipItem(SlotLocation location, GameObject itemPrefab)
    {
        if (m_SocketDic.TryGetValue(location, out GameObject socket))
        {
            foreach (Transform child in socket.transform)
            {
                Destroy(child.gameObject);
            }
        }

        if (itemPrefab)
        {
            GameObject newItem = Instantiate(itemPrefab, socket.transform);
        }
    }
}
