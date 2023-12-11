using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarFlashing : MonoBehaviour {



    private const string FLASHING = "IsFlashing";


    [SerializeField] private StoveCounter stoveCounter;

    [SerializeField] private Image progressBarImage;

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        stoveCounter.CuttingProgressEvent += StoveCounter_CuttingProgressEvent;

        animator.SetBool(FLASHING, false);
    }

    private void StoveCounter_CuttingProgressEvent(object sender, IProgressBar.CuttingProgressEventArgs e) {

        float burnShowProgressAmount = 0.5f;

        bool show = stoveCounter.isFried() && e.ProgressBarAmount >= burnShowProgressAmount;


        animator.SetBool(FLASHING, show);

        if (!show) {
            progressBarImage.color = new Color(0, 255, 34, 255);
        }


    }




}
