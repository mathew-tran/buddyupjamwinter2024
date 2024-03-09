using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GAME_STATE
{
    INIT,
    PLAYING,
    END
}
public class Game : MonoBehaviour
{
    public GAME_STATE mCurrentGameState;

    public Action OnGameStart;
    public Action OnGameEnd;

    public Timer mStartGameTimer;
    public Timer mEndGameTimer;

    public bool mBIsGarbageDone = false;

    void Start()
    {
        mCurrentGameState = GAME_STATE.INIT;
        mStartGameTimer.OnTimeout += StartGame;
        mStartGameTimer.StartTimer();

        mEndGameTimer.OnTimeout += FinishGame;

        GameManager.GetGarbageHolder().OnGarbageComplete += OnGameGarbageComplete;
        GameManager.GetCogholder().OnCogsComplete += OnGameCogsComplete;
    }


    private void StartGame()
    {
        mCurrentGameState = GAME_STATE.PLAYING;
        OnGameStart();
    }

    private void OnGameGarbageComplete()
    {
        mBIsGarbageDone = true;
    }

    private void OnGameCogsComplete()
    {
        mEndGameTimer.StartTimer();
    }

    private void StartFinishingGame()
    {
        mEndGameTimer.StartTimer();
    }

    private void FinishGame()
    {
        mCurrentGameState = GAME_STATE.END;
        OnGameEnd();
    }

   
}
