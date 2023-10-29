using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtProgressBar : MonoBehaviour {


    public enum LookAt {
        LookAt,
        LookAtInverted,
        forward,
        forwardInverted
    }

    [SerializeField] private LookAt lookAt;

    private void Update() {
        switch (lookAt) {
            case LookAt.LookAt:
                transform.LookAt(Camera.main.transform.position);
                break;
            case LookAt.LookAtInverted:
                Vector3 camDirection = Camera.main.transform.position;
                transform.LookAt(transform.position - camDirection);
                break;
            case LookAt.forward:
                transform.forward = Camera.main.transform.forward;
                break;
            case LookAt.forwardInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
        }
    }


}
