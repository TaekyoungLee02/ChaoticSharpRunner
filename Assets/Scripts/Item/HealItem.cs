using UnityEngine;

public class HealItem : ItemBase
{
    public override void Use(Player player)
    {
        player.health.Heal((int)itemValue);
    }
}
