using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string nameSound;
    [Range(0, 1f)]
    public float volume;
	[Range(0, 1f)]
	public float pitch;
    public AudioClip clip;
}
public class AudioManager : MonoBehaviour
{
    public List<Sound> soundList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAudio(string audioName)
    {
        Sound sound = soundList.Find(s => s.nameSound == audioName);
        if (sound != null)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = sound.clip;
            audioSource.volume = sound.volume;
            audioSource.pitch = sound.pitch;
            audioSource.Play();

            Destroy(audioSource, sound.clip.length);
        }
    }

    public void StopAllAudio()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        foreach(AudioSource a in audioSources)
        {
            if (a.clip.name.Contains("Background")) continue;
            Destroy(a);
        }
    }
}
