using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : Singleton<AudioManager>
{
    private static GameObject s_SoundObject;
    private static AudioClip[] s_AudioClipList;
    private static Queue<GameObject> s_Pool;

    private AudioSource m_BgmAudioSource;

    public override void Awake()
    {
        s_SoundObject = Resources.Load<GameObject>("Prefabs/AudioSource");
        s_AudioClipList = Resources.LoadAll<AudioClip>("Audios");
        s_Pool = new Queue<GameObject>();

        base.Awake();
        m_BgmAudioSource = GetComponent<AudioSource>();
        m_BgmAudioSource.loop = true;
    }

    public AudioClip GetAudioClip (AudioClipName name)
    {
        int index = (int)name;
        return s_AudioClipList[index];
    }

    public void PlaySoundFXClip (AudioClipName name, Vector3 soundPos, float volume)
    {
        AudioClip clip = GetAudioClip(name);
        PlaySoundFXClip(clip, soundPos, volume);
    }

    public void PlaySoundFXClip (AudioClip clip, Vector3 soundPos, float volume)
    {
        AudioController controller = SpawnSoundObject();
        controller.transform.position = soundPos;
        controller.AudioSource.clip = clip;
        controller.AudioSource.volume = volume;
        controller.gameObject.SetActive(true);
        controller.PlayAndDespawn();
    }

    public void PlayBGMClip (AudioClipName name, float volume)
    {
        AudioClip clip = GetAudioClip(name);
        PlayBGMClip(clip, volume);
    }

    public void PlayBGMClip (AudioClip clip, float volume)
    {
        m_BgmAudioSource.Stop();
        m_BgmAudioSource.clip = clip;
        m_BgmAudioSource.volume = volume;
        m_BgmAudioSource.Play();
    }

    public void StopBackgroundMusic ()
    {
        m_BgmAudioSource.Stop();
    }

    public AudioController SpawnSoundObject ()
    {
        if (s_Pool.Count == 0)
        {
            GameObject newAudioSource = Instantiate(s_SoundObject, transform);
            newAudioSource.name = "AudioSource";
            newAudioSource.gameObject.SetActive(false);
            s_Pool.Enqueue(newAudioSource);
        }

        GameObject obj = s_Pool.Dequeue();
        return obj.GetComponent<AudioController>();
    }

    public void DespawnSoundObject (GameObject soundObj)
    {
        soundObj.SetActive(false);
        s_Pool.Enqueue(soundObj);
    }
}
