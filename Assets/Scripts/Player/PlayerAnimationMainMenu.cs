using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationMainMenu : MonoBehaviour {

    private Animator _animator;
    private const string IS_MOVING = "IsMoving";

    private void Start() {

        _animator = GetComponent<Animator>();

    }


    private void Update() {
        HandlePlayerAnimation();
    }


    private void HandlePlayerAnimation() {
        _animator.SetBool(IS_MOVING, false);
    }



}
