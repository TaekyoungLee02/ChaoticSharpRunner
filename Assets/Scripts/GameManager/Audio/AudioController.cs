using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    private AudioSource m_AudioSource;
    private float m_ClipLength;

    public AudioSource AudioSource => m_AudioSource;

    private void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    public void PlayAndDespawn ()
    {
        m_AudioSource.Play();
        StartCoroutine(AutoDespawn());
    }

    private IEnumerator AutoDespawn ()
    {
        m_ClipLength = m_AudioSource.clip.length;
        yield return new WaitForSeconds(m_ClipLength);
        AudioManager.Instance.DespawnSoundObject(gameObject);
    }
}
