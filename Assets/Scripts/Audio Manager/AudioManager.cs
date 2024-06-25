using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip[] musicClips;
    private const string MutePrefKey = "MuteState";

    private void Awake()
    {
        LoadMuteState();
    }

    public void PlayMusic(int index)
    {
        if (index >= 0 && index < musicClips.Length)
        {
            AudioClip clip = musicClips[index];
            if (musicSource.clip != clip)
            {
                musicSource.clip = clip;
                musicSource.Play();
            }
        }
    }
    public void StopMusic()
    {
        musicSource.Stop();
    }
    public void MuteAll()
    {
        musicSource.mute = true;
        PlayerPrefs.SetInt(MutePrefKey, 1);
        PlayerPrefs.Save();
    }
    public void UnmuteAll()
    {
        musicSource.mute = false;
        PlayerPrefs.SetInt(MutePrefKey, 0);
        PlayerPrefs.Save();
    }
    public void LoadMuteState()
    {
        int muteState = PlayerPrefs.GetInt(MutePrefKey, 0);
        musicSource.mute = (muteState == 1);
    }
}
