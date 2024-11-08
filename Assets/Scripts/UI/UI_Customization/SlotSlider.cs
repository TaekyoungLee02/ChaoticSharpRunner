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

        //m_CustomizedData = Resources.Load<CustomizedDataSO>("Data/CustomizedData");
        m_CustomizedData = Global.customData;
    }

    private void ChangeAccessory(float floatValue)
    {
        int value = (int)floatValue;
        var data = m_AccessoryDataList[value];

        GameObject prefab;

        if (data == null)
            prefab = null;
        else
            prefab = data.prefab;

        m_CustomizationController.EquipAccessory(m_SlotLocation, prefab);

        switch (m_SlotLocation) {
        case SlotLocation.Head:
            m_CustomizedData.headSlot = prefab;
            break;

        case SlotLocation.Back:
            m_CustomizedData.backSlot = prefab;
            break;
        }
    }
}
