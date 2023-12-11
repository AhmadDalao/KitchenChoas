using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance { get; private set; }

    private const string PLAYER_PREFS_SOUND_EFFECT_VOLUME = "SoundEffectVolume";



    [SerializeField] private AudioClipRefScriptable _audioClipRef;


    private float volume = 0.9f;

    private void Awake() {

        if (Instance != null) {
            Debug.Log("there is more than 1 Instance in Sound Manager");
        }

        Instance = this;


        // the 0.9f is the fallback data just in case.
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECT_VOLUME, 0.9f);

    }

    private void Start() {
        DeliveryManager.Instance.OnRecipeFailed += Instance_OnRecipeFailed;
        DeliveryManager.Instance.OnRecipeSuccess += Instance_OnRecipeSuccess;
        CuttingCounter.OnAnyCutSoundEvent += CuttingCounter_OnAnyCutSoundEvent;
        // player picked up something
        Player.Instance.OnPlayerPickedSomethingEvent += Instance_OnPlayerPickedSomethingEvent;
        // on drop anything
        BaseCounter.OnAnyDropEvent += BaseCounter_OnAnyDropEvent;
        // on destroy
        TrashCounter.OnTrashEvent += TrashCounter_OnTrashEvent;
    }

    private void TrashCounter_OnTrashEvent(object sender, System.EventArgs e) {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(_audioClipRef.GetTrash(), trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyDropEvent(object sender, System.EventArgs e) {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(_audioClipRef.GetObjectDrop(), baseCounter.transform.position);
    }

    private void Instance_OnPlayerPickedSomethingEvent(object sender, System.EventArgs e) {
        PlaySound(_audioClipRef.GetObjectPickup(), Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCutSoundEvent(object sender, System.EventArgs e) {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(_audioClipRef.GetChop(), cuttingCounter.transform.position);
    }

    private void Instance_OnRecipeSuccess(object sender, System.EventArgs e) {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(_audioClipRef.GetDeliverySuccess(), deliveryCounter.transform.position);
    }

    private void Instance_OnRecipeFailed(object sender, System.EventArgs e) {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(_audioClipRef.GetDeliveryFail(), deliveryCounter.transform.position);
    }


    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volumeMultiplayer = 1f) {
        AudioSource.PlayClipAtPoint(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volumeMultiplayer * volume);
    }


    public void PlayFootSteps(Vector3 position, float volume = 1f) {
        PlaySound(_audioClipRef.GetFootSteps(), position, volume);
    }

    public void PlayCountDownSound() {
        PlaySound(_audioClipRef.GetWarning(), Vector3.zero);
    }

    public void PlayStoveWarningSound(Vector3 position) {
        PlaySound(_audioClipRef.GetWarning(), position);
    }


    public void ChangeVolume() {
        volume += 0.1f;

        if (volume > 1f) {
            volume = 0f;
        }


        // save the changes using unity prefs
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECT_VOLUME, volume);
        // save data in case unity crashed
        PlayerPrefs.Save();

    }

    public float GetVolume() {
        return volume;
    }


}



