using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProgressBar {


    public event EventHandler<CuttingProgressEventArgs> CuttingProgressEvent;

    public class CuttingProgressEventArgs : EventArgs {
        public float ProgressBarAmount;
    }


}
