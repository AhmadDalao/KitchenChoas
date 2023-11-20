using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AudioClipRefScriptable : ScriptableObject {


    [SerializeField] private AudioClip[] chop;
    [SerializeField] private AudioClip[] deliveryFail;
    [SerializeField] private AudioClip[] deliverySuccess;
    [SerializeField] private AudioClip[] footSteps;
    [SerializeField] private AudioClip[] objectDrop;
    [SerializeField] private AudioClip[] objectPickup;
    [SerializeField] private AudioClip stoveSizzle;
    [SerializeField] private AudioClip[] trash;
    [SerializeField] private AudioClip[] warning;


    public AudioClip[] GetChop() {
        return chop;
    }

    public AudioClip[] GetDeliveryFail() {
        return deliveryFail;
    }

    public AudioClip[] GetDeliverySuccess() {
        return deliverySuccess;
    }

    public AudioClip[] GetFootSteps() {
        return footSteps;
    }

    public AudioClip[] GetObjectDrop() {
        return objectDrop;
    }

    public AudioClip[] GetObjectPickup() {
        return objectPickup;
    }

    public AudioClip GetStoveSizzle() {
        return stoveSizzle;
    }

    public AudioClip[] GetTrash() {
        return trash;
    }

    public AudioClip[] GetWarning() {
        return warning;
    }

}
