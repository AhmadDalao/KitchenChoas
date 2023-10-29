using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveOnVisual : MonoBehaviour {
    [SerializeField] private GameObject[] _stoveOnEffect;
    [SerializeField] private StoveCounter _stoveCounter;
    // Start is called before the first frame update
    void Start() {
        _stoveCounter.StoveOnVisualEvent += _stoveCounter_StoveOnVisualEvent; ;
    }

    private void _stoveCounter_StoveOnVisualEvent(object sender, StoveCounter.StoveOnVisualEventArgs e) {

        if (e.stoveState == StoveCounter.State.Frying || e.stoveState == StoveCounter.State.Fried) {
            ShowStoveVisual();
        } else {
            HideStoveVisual();
        }

    }

    private void ShowStoveVisual() {
        foreach (var item in _stoveOnEffect) {
            item.SetActive(true);
        }
    }

    private void HideStoveVisual() {
        foreach (var item in _stoveOnEffect) {
            item.SetActive(false);
        }
    }


}
