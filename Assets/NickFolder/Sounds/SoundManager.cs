using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] List<AudioSource> audioSources;
    [SerializeField] List<SoundEnums> soundEnums;
    Dictionary<SoundEnums, AudioSource> map;

    // Start is called before the first frame update
    void Start()
    {
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
}
