using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerAnimation : MonoBehaviour {

    [SerializeField] private ContainerCounter _containerCounter;
    private Animator _animator;
    private const string OPEN_CLOSE = "OpenClose";


    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start() {
        _containerCounter.ContainerAnimationEvent += _containerCounter_ContainerAnimationEvent;
    }

    private void _containerCounter_ContainerAnimationEvent(object sender, System.EventArgs e) {

        _animator.SetTrigger(OPEN_CLOSE);

    }


}
