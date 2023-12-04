using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }

    public enum GameState {
        waitingToPlay,
        waitingToPlayCountDown,
        playingTime,
        gameOver,
    }

    public event EventHandler OnGameStateChange;

    public event EventHandler ShowPausedUI;
    public event EventHandler HidePausedUI;

    private GameState gameState;

    private float waitingToPlayTimer = 1f;
    private float waitingToPlayCountDown = 3f;
    private float playingTimeTimer;
    private float playingTimeTimerMax = 10f; // change it later

    private bool isGamePaused = false;


    private void Awake() {

        if (Instance != null) {
            Debug.Log("GameManager has more than 1 instance");
        }

        Instance = this;
    }

    private void Start() {
        GameInputManager.Instance.GamePauseEvent += Instance_GamePauseEvent;
    }

    private void Instance_GamePauseEvent(object sender, EventArgs e) {
        TogglePauseGame();
    }

    private void Update() {
        switch (gameState) {
            case GameState.waitingToPlay:
                waitingToPlayTimer -= Time.deltaTime;
                Debug.Log("Waiting To Play Timer");
                if (waitingToPlayTimer < 0f) {
                    gameState = GameState.waitingToPlayCountDown;
                    OnGameStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;
            case GameState.waitingToPlayCountDown:
                waitingToPlayCountDown -= Time.deltaTime;
                Debug.Log("Count Time before playing");
                if (waitingToPlayCountDown < 0f) {
                    gameState = GameState.playingTime;
                    playingTimeTimer = playingTimeTimerMax;
                    OnGameStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;
            case GameState.playingTime:
                playingTimeTimer -= Time.deltaTime;
                Debug.Log("Now Playing !! ");
                if (playingTimeTimer < 0f) {
                    gameState = GameState.gameOver;
                    OnGameStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;
            case GameState.gameOver:
                Debug.Log("game over !! cant play now");
                break;
        }
    }
    // didn't use it yet
    public bool IsWaitingToPlay() {
        return gameState == GameState.waitingToPlay;
    }

    public bool IsPlayingTimeState() {
        return gameState == GameState.playingTime;
    }

    public float GetGamePlayingCountDown() {
        return 1 - (playingTimeTimer / playingTimeTimerMax);
    }

    public bool IsWaitingToPlayCountDown() {
        return gameState == GameState.waitingToPlayCountDown;
    }

    public float GetWaitingToPlayCountDown() {
        return waitingToPlayCountDown;
    }

    public bool IsGameOver() {
        return gameState == GameState.gameOver;
    }


    public void TogglePauseGame() {
        isGamePaused = !isGamePaused;
        if (isGamePaused) {
            Time.timeScale = 0f;
            ShowPausedUI?.Invoke(this, EventArgs.Empty);
        } else {
            Time.timeScale = 1f;
            HidePausedUI?.Invoke(this, EventArgs.Empty);
        }
    }



}
