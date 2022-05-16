using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentSceneMusic : MonoBehaviour
{
    public AudioClip mainSong;
    public AudioClip altSong;

    private void Start()
    {
        AudioManager.instance.PlayMusic(mainSong, 3);
    }

}
