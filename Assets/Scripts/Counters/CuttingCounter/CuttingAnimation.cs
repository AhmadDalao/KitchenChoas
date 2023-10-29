using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingAnimation : MonoBehaviour {

    [SerializeField] private CuttingCounter _cuttingCounter;
    private Animator _animator;
    private const string CUT = "Cut";

    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    private void Start() {
        _cuttingCounter.CuttingAnimationEvent += _cuttingCounter_CuttingAnimationEvent;
    }

    private void _cuttingCounter_CuttingAnimationEvent(object sender, System.EventArgs e) {

        _animator.SetTrigger(CUT);

    }
}
