using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioSource sfxSource;
    public AudioClip[] sfxClips;
    private const string MutePrefKey = "SFXMuteState";

    private void Awake()
    {
        LoadMuteState();
    }

    public void PlaySFX(int index)
    {
        if (index >= 0 && index < sfxClips.Length)
        {
            sfxSource.PlayOneShot(sfxClips[index]);
        }
    }

    public void MuteAll()
    {
        sfxSource.mute = true;
        PlayerPrefs.SetInt(MutePrefKey, 1);
        PlayerPrefs.Save();
    }

    public void UnmuteAll()
    {
        sfxSource.mute = false;
        PlayerPrefs.SetInt(MutePrefKey, 0);
        PlayerPrefs.Save();
    }

    public void LoadMuteState()
    {
        int muteState = PlayerPrefs.GetInt(MutePrefKey, 0);
        sfxSource.mute = (muteState == 1);
    }
}
