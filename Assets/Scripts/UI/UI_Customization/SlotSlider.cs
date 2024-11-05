using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SlotSlider : MonoBehaviour
{
    [SerializeField] private UnityChanSlots m_SlotManager;
    [SerializeField] SlotLocation m_SlotLocation;
    [SerializeField] List<GameObject> m_Prefabs;

    private Slider m_Slider;
    private CustomizedDataSO m_CustomizedData;

    private void Awake()
    {
        m_Slider = GetComponent<Slider>();
        m_Slider.onValueChanged.AddListener(ChangeAccessory);
        m_Slider.maxValue = m_Prefabs.Count - 1;

        m_CustomizedData = Resources.Load<CustomizedDataSO>("Data/CustomizedData");
    }

    private void ChangeAccessory(float floatValue)
    {
        int value = (int)floatValue;

        if (m_Prefabs == null)
        {
            return;
        }

        if (value < 0 || value >= m_Prefabs.Count)
        {
            return;
        }

        switch (m_SlotLocation)
        {
        case SlotLocation.Head:
            m_CustomizedData.headSlot = m_Prefabs[value];
            break;

        case SlotLocation.Back:
            m_CustomizedData.backSlot = m_Prefabs[value];
            break;

        default:
            break;
        }

        m_SlotManager.EquipItem(m_SlotLocation, m_Prefabs[value]);
    }
}
