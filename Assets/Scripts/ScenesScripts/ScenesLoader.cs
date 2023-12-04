using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ScenesLoader {


    public enum Scene {
        GameScene,
        LoadingScene,
        MainMenuScene
    }

    private static Scene targetScene;


    public static void Load(Scene sceneToLoad) {
        targetScene = sceneToLoad;
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void LoaderCallback() {
        SceneManager.LoadScene(targetScene.ToString());
    }

}
