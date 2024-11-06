using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class SuperArmorItem : ItemBase
{
    public override void Use(Player player)
    {
        base.Use(player);

        AudioManager.Instance.PlaySoundFXClip(AudioClipName.Sfx_26, transform.position, 0.5f);
        AudioManager.Instance.PlaySoundFXClip(AudioClipName.UnityChan_Soret, transform.position, 0.5f);

        player.stats.StartSuperArmor(duration);
    }
}