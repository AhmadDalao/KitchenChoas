using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderCallback : MonoBehaviour {



    private bool isLoading = true;

    private void Update() {
        if (isLoading) {
            isLoading = false;
            ScenesLoader.LoaderCallback();
        }
    }



}
