using UnityEngine;
using UnityEngine.UI;

public class MusicSliderController : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    private AudioSource bgmAudioSource;

    private void Start()
    {
        // AudioManager 싱글톤 인스턴스를 통해 BGM 오디오 소스를 가져옴
        bgmAudioSource = AudioManager.Instance.GetComponent<AudioSource>();

        // 초기 슬라이더 값 설정
        if (bgmAudioSource != null)
        {
            musicSlider.value = bgmAudioSource.volume;
        }

        // 슬라이더 이벤트에 메서드 연결
        musicSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        if (bgmAudioSource != null)
        {
            // 슬라이더 값에 따라 BGM의 볼륨 설정
            bgmAudioSource.volume = value;
        }
    }
}
