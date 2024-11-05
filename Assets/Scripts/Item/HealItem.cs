using UnityEngine;

public class HealItem : ItemBase
{
    public override void Use(Player player)
    {
        base.Use(player);

        player.stats.Heal((int)itemValue);
    }
}
