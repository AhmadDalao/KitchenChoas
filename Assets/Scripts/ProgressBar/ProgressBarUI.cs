using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour {

    [SerializeField] private BaseCounter _baseCounter;
    [SerializeField] private Image _progressBarImage;
    [SerializeField] private GameObject _progressBarObject;

    private IProgressBar _iprogressBar;


    private void Start() {
        // get interface reference from baseCounter.
        _iprogressBar = _baseCounter.GetComponent<IProgressBar>();
        // now subscribe to the event
        _iprogressBar.CuttingProgressEvent += _iprogressBar_CuttingProgressEvent;
        // reset the progress bar amount
        _progressBarImage.fillAmount = 0f;
        // hide the progress bar
        HideProgressBar();
    }

    private void _iprogressBar_CuttingProgressEvent(object sender, IProgressBar.CuttingProgressEventArgs e) {
        _progressBarImage.fillAmount = e.ProgressBarAmount;

        if (_progressBarImage.fillAmount == 0f || _progressBarImage.fillAmount == 1f) {
            HideProgressBar();
        } else {
            ShowProgressBar();
        }
    }

    private void HideProgressBar() {
        _progressBarObject.gameObject.SetActive(false);
    }

    private void ShowProgressBar() {
        _progressBarObject.gameObject.SetActive(true);
    }

}
