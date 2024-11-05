using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SlotSlider : MonoBehaviour
{
    [SerializeField] private CustomizationController m_CustomizationController;
    [SerializeField] SlotLocation m_SlotLocation;
    [SerializeField] List<AccessoryDataSO> m_AccessoryDataList;

    private Slider m_Slider;
    private CustomizedDataSO m_CustomizedData;

    private void Awake()
    {
        m_Slider = GetComponent<Slider>();
        m_Slider.onValueChanged.AddListener(ChangeAccessory);

        m_AccessoryDataList = AccessoryManager.GetListBySlot(m_SlotLocation);
        m_Slider.maxValue = m_AccessoryDataList.Count;
        m_AccessoryDataList.Insert(0, null);

        m_CustomizedData = Resources.Load<CustomizedDataSO>("Data/CustomizedData");
    }

    private void ChangeAccessory(float floatValue)
    {
        int value = (int)floatValue;
        var data = m_AccessoryDataList[value];

        if (data == null)
            m_CustomizationController.EquipAccessory(m_SlotLocation, null);
        else
            m_CustomizationController.EquipAccessory(m_SlotLocation, data.prefab);
    }
}
