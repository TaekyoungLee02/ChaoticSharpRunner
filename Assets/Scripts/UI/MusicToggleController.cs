using UnityEngine;
using UnityEngine.UI;

public class MusicSliderController : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    private AudioSource bgmAudioSource;

    private void Start()
    {
        // AudioManager �̱��� �ν��Ͻ��� ���� BGM ����� �ҽ��� ������
        bgmAudioSource = AudioManager.Instance.GetComponent<AudioSource>();

        // �ʱ� �����̴� �� ����
        if (bgmAudioSource != null)
        {
            musicSlider.value = bgmAudioSource.volume;
        }

        // �����̴� �̺�Ʈ�� �޼��� ����
        musicSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        if (bgmAudioSource != null)
        {
            // �����̴� ���� ���� BGM�� ���� ����
            bgmAudioSource.volume = value;
        }
    }
}
