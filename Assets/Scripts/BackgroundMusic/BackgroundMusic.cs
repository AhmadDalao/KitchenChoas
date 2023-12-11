using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour {


    public static BackgroundMusic Instance { get; private set; }

    private const string PLAYER_PREFS_BACKGROUND_VOLUME = "MusicBackground";


    private AudioSource audioSource;

    private float volume = 0.5f;

    private void Awake() {

        if (Instance != null) {
            Debug.Log("there is more than 1 background music manager");
        }

        Instance = this;


        audioSource = GetComponent<AudioSource>();

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_BACKGROUND_VOLUME, 0.5f);

        audioSource.volume = volume;
    }


    public void ChangeMusicVolume() {
        volume += 0.1f;
        if (volume > 1.1f) {
            volume = 0f;
        }
        audioSource.volume = volume;

        // save to unity 

        PlayerPrefs.SetFloat(PLAYER_PREFS_BACKGROUND_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume() {
        return volume;
    }

}
