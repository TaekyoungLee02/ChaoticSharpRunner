using UnityEngine;

public class HealItem : ItemBase
{
    public override void Use(Player player)
    {
        player.stats.Heal((int)itemValue);
    }
}
