using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] List<AudioSource> audioSources;
    [SerializeField] List<SoundEnums> soundEnums;
    [SerializeField] AudioMixer sfxMixer;
    Dictionary<SoundEnums, AudioSource> map;
    [SerializeField] int minSound;
    [SerializeField] int maxSound;

    public static SoundManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }

        Instance = this;
        map = new Dictionary<SoundEnums, AudioSource>();

        for (int i = 0; i < audioSources.Count; i++)
        {
            map[soundEnums[i]] = audioSources[i];
        }
    }

    public void PlaySound(SoundEnums soundEnum)
    {
        if (!map[soundEnum].isPlaying)
        {
            map[soundEnum].Play();
        }
    }

    public void StopSound(SoundEnums soundEnum)
    {
        map[soundEnum].Stop();
    }

    public void SetVolumeSFX(int volume)
    {
        sfxMixer.SetFloat("Volume", Mathf.Log10(Mathf.Max(volume / 10.0f, .00001f)) * 20);
        Debug.Log(Mathf.Log10(Mathf.Max(volume / 100.0f, .00001f)) * 20);
    }
}
