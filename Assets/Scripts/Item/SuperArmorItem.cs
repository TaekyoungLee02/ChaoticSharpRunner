using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class SuperArmorItem : ItemBase
{
    public override void Use(Player player)
    {
        base.Use(player);

        player.stats.StartSuperArmor(duration);
    }
}