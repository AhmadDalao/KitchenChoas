using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterSelectVisual : MonoBehaviour {
    [SerializeField] private BaseCounter _baseCounter;
    [SerializeField] private GameObject[] _counterSelect;




    private void Start() {
        Player.Instance.CounterSelectVisualEvent += Instance_CounterSelectVisualEvent;
    }

    private void Instance_CounterSelectVisualEvent(object sender, Player.CounterSelectVisualEventArgs e) {
        if (e.BaseCounterSelectVisual == _baseCounter) {
            ShowVisual();
        } else {
            HideVisual();
        }

    }


    private void ShowVisual() {
        foreach (var item in _counterSelect) {
            item.SetActive(true);
        }
    }


    private void HideVisual() {
        foreach (var item in _counterSelect) {
            item.SetActive(false);
        }
    }


}
