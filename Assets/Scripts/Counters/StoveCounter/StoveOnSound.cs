using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveOnSound : MonoBehaviour {

    [SerializeField] private StoveCounter _stoveCounter;

    private AudioSource m_SoundSource;


    private float warningSoundTimer;

    private bool playWarningSound;

    private void Awake() {
        m_SoundSource = GetComponent<AudioSource>();
    }

    private void Start() {
        _stoveCounter.StoveOnVisualEvent += _stoveCounter_StoveOnVisualEvent;
        _stoveCounter.CuttingProgressEvent += _stoveCounter_CuttingProgressEvent;
    }

    private void _stoveCounter_CuttingProgressEvent(object sender, IProgressBar.CuttingProgressEventArgs e) {
        float burnShowProgressAmount = 0.5f;

        playWarningSound = _stoveCounter.isFried() && e.ProgressBarAmount >= burnShowProgressAmount;

    }

    private void _stoveCounter_StoveOnVisualEvent(object sender, StoveCounter.StoveOnVisualEventArgs e) {

        bool playSound = e.stoveState == StoveCounter.State.Fried || e.stoveState == StoveCounter.State.Frying;

        if (playSound) {
            m_SoundSource.Play();
        } else {
            m_SoundSource.Pause();
        }
    }

    private void Update() {
        PlayWarningSound();
    }

    private void PlayWarningSound() {
        if (playWarningSound) {
            warningSoundTimer -= Time.deltaTime;
            float warningSoundTimerMax = 0.2f;
            if (warningSoundTimer <= 0f) {
                warningSoundTimer = warningSoundTimerMax;
                SoundManager.Instance.PlayStoveWarningSound(_stoveCounter.transform.position);
            }
        }
    }
}
