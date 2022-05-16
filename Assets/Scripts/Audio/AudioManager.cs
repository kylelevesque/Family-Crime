using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public enum AudioChannel { Master, Music, SFX }

    public float masterVolumePercent {get; private set; } =  0.3f;
    public float sfxVolumePercent {get; private set; } = 1;
    public float musicVolumePercent { get; private set; } = 1;

    AudioSource[] musicSources;
    int activeMusicSourceIndex;

    Transform audioListenerT;
    Transform playerT;

    private void Awake()
    {
        //Singleton Set-up
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        //Audio Source instantiation
        musicSources = new AudioSource[2];
        for(int i = 0; i < 2; i++)
        {
            GameObject newMusicSource = new GameObject("Music Source " + (i+1));
            musicSources[i] = newMusicSource.AddComponent<AudioSource>();
            newMusicSource.transform.parent = transform;
        }

        audioListenerT = FindObjectOfType<AudioListener>().transform;
        if(GameObject.FindGameObjectWithTag("Player") != null)
            playerT = GameObject.FindGameObjectWithTag("Player").transform;

        //tracking player volume preferences between sessions
        masterVolumePercent = PlayerPrefs.GetFloat("master vol", masterVolumePercent);
        musicVolumePercent = PlayerPrefs.GetFloat("music vol", musicVolumePercent);
        sfxVolumePercent = PlayerPrefs.GetFloat("sfx vol", sfxVolumePercent);

    }

    private void Update()
    {
        if (playerT != null)
        {
            audioListenerT.position = playerT.position;
        }
    }

    public void PlaySound(AudioClip clip, Vector2 pos)
    {
        AudioSource.PlayClipAtPoint(clip, pos, sfxVolumePercent * masterVolumePercent * 10);
    }

    public void PlayMusic(AudioClip clip, float fadeDuration = 1)
    {
        activeMusicSourceIndex = 1 - activeMusicSourceIndex;
        musicSources[activeMusicSourceIndex].clip = clip;
        musicSources[activeMusicSourceIndex].loop = true;
        musicSources[activeMusicSourceIndex].Play();

        StartCoroutine(AnimateMusicCrossfade(fadeDuration));
    }

    IEnumerator AnimateMusicCrossfade(float duration)
    {
        float percent = 0;

        while(percent < 1)
        {
            percent += Time.deltaTime * 1 / duration;
            musicSources[activeMusicSourceIndex].volume = Mathf.Lerp(0, musicVolumePercent * masterVolumePercent, percent);
            musicSources[1-activeMusicSourceIndex].volume = Mathf.Lerp(musicVolumePercent * masterVolumePercent, 0, percent);
            yield return null;
        }
    }

    public void SetAudioVolume(float volumePercent, AudioChannel channel)
    {
        switch (channel)
        {
            case AudioChannel.Master:
                masterVolumePercent = volumePercent;
                break;
            case AudioChannel.Music:
                musicVolumePercent = volumePercent;
                break;
            case AudioChannel.SFX:
                sfxVolumePercent = volumePercent;
                break;
        }

        musicSources[0].volume = musicVolumePercent * masterVolumePercent;
        musicSources[1].volume = musicVolumePercent * masterVolumePercent;

        PlayerPrefs.SetFloat("master vol", masterVolumePercent);
        PlayerPrefs.SetFloat("music vol", musicVolumePercent);
        PlayerPrefs.SetFloat("sfx vol", sfxVolumePercent);
        PlayerPrefs.Save();

    }
}
