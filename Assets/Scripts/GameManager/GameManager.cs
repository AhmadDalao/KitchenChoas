using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


    public static GameManager Instance { get; private set; }

    public event EventHandler OnGameStateChange;

    public enum State {
        waitingForGame,
        GameCountDown,
        GamePlaying,
        GameOver
    }


    private State state;


    private float waitingForGameTimer = 1f;
    private float GameCountDownTimer = 3f;
    private float GamePlayingTimer;
    private float GamePlayingTimerMax = 40f; // change time limit later just for testing leaving it as 10 seconds


    private void Awake() {

        if (Instance != null) {
            Debug.Log("Game Manager has more than 1 instance");
        }

        Instance = this;
    }

    private void Update() {

        switch (state) {
            case State.waitingForGame:
                waitingForGameTimer -= Time.deltaTime;
                if (waitingForGameTimer < 0f) {
                    state = State.GameCountDown;
                    OnGameStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameCountDown:
                GameCountDownTimer -= Time.deltaTime;
                if (GameCountDownTimer < 0f) {
                    state = State.GamePlaying;
                    GamePlayingTimer = GamePlayingTimerMax;
                    OnGameStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                GamePlayingTimer -= Time.deltaTime;
                if (GamePlayingTimer < 0f) {
                    state = State.GameOver;
                    OnGameStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }

    }


    public bool GetIsPlaying() {
        return state == State.GamePlaying;
    }

    public bool GetGameStateIsCountDown() {
        return state == State.GameCountDown;
    }

    public bool GetIsGameOver() {
        return state == State.GameOver;
    }

    public float GetGameCountDownTimer() {
        return GameCountDownTimer;
    }

    public float GetGamePlayingTimer() {
        return 1 - (GamePlayingTimer / GamePlayingTimerMax);
    }

}
