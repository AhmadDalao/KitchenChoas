using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoveBurningWarningUI : MonoBehaviour {


    [SerializeField] private StoveCounter stoveCounter;


    private void Start() {
        stoveCounter.CuttingProgressEvent += StoveCounter_CuttingProgressEvent;


        Hide();
    }

    private void StoveCounter_CuttingProgressEvent(object sender, IProgressBar.CuttingProgressEventArgs e) {

        float burnShowProgressAmount = 0.5f;

        bool show = stoveCounter.isFried() && e.ProgressBarAmount >= burnShowProgressAmount;

        if (show) {
            Show();
            SoundManager.Instance.PlayCountDownSound();
        } else {
            Hide();
        }

    }


    private void Hide() {
        gameObject.SetActive(false);
    }

    private void Show() {
        gameObject.SetActive(true);
    }

}
