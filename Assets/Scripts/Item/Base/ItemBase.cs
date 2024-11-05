using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private string itemDescription;

    [SerializeField] protected float itemValue;
    [SerializeField] protected float duration;

    public virtual void Init(ItemSO so)
    {
        itemName = so.itemName;
        itemDescription = so.itemDescription;
        itemValue = so.itemValue;
        duration = so.duration;
    }

    //Player 매개변수로 받음
    public virtual void Use(Player player)
    {
        ItemManager.Instance.ItemUse();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Use(collision.gameObject.GetComponent<Player>());
            gameObject.SetActive(false);
        }
    }
}
