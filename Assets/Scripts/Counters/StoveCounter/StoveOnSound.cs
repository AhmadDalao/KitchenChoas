using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveOnSound : MonoBehaviour {

    [SerializeField] private StoveCounter _stoveCounter;

    private AudioSource m_SoundSource;

    private void Awake() {
        m_SoundSource = GetComponent<AudioSource>();
    }

    private void Start() {
        _stoveCounter.StoveOnVisualEvent += _stoveCounter_StoveOnVisualEvent;
    }

    private void _stoveCounter_StoveOnVisualEvent(object sender, StoveCounter.StoveOnVisualEventArgs e) {
        bool playSound = e.stoveState == StoveCounter.State.Fried || e.stoveState == StoveCounter.State.Frying;

        if (playSound) {
            m_SoundSource.Play();
        } else {
            m_SoundSource.Pause();
        }

    }


}
