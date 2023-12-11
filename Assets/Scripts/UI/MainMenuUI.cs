using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {



    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;


    [SerializeField] private EventSystem eventSystem;


    private void Awake() {


        eventSystem.SetSelectedGameObject(null);
        eventSystem.SetSelectedGameObject(playButton.gameObject);


        playButton.onClick.AddListener(() => {
            ScenesLoader.Load(ScenesLoader.Scene.GameScene);
        });

        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });

        Time.timeScale = 1f;
    }




}
