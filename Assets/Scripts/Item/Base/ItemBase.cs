using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    private string itemName;
    private string itemDescription;

    protected float itemValue;
    protected float duration;

    public virtual void Init(ItemSO so)
    {
        itemName = so.itemName;
        itemDescription = so.itemDescription;
        itemValue = so.itemValue;
        duration = so.duration;
    }

    public abstract void Use(Player player); //Player 매개변수로 받음

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Use(collision.gameObject.GetComponent<Player>());
            // 풀로 돌려놓기
        }
    }
}
