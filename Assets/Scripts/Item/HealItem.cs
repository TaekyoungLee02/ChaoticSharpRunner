using UnityEngine;

public class HealItem : ItemBase
{
    public override void Use(Player player)
    {
        base.Use(player);

        AudioManager.Instance.PlaySoundFXClip(AudioClipName.Sfx_15, transform.position, 0.5f);


        player.stats.Heal((int)itemValue);
    }
}
