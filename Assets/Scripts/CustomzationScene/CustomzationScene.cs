using UnityEngine;

public class CustomzationScene : MonoBehaviour {
    private void Start () {
        AudioManager.Instance.PlayBGMClip(AudioClipName.Bgm_Peaceful, 0.3f);
    }
}
