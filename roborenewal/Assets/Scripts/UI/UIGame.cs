using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGame : MonoBehaviour
{
    public GameObject mStartScreen;
    public GameObject mEndScreen;
    public GameObject mPlayScreen;
    void Start()
    {
        mStartScreen.SetActive(true);
        mEndScreen.SetActive(false);
        mPlayScreen.SetActive(false);

        GameManager.GetGame().OnGameStart += OnUIGameStart;
        GameManager.GetGame().OnGameEnd += OnUIGameEnd;
    }

    private void OnUIGameStart()
    {
        mStartScreen.SetActive(false);
        mPlayScreen.SetActive(true);
    }

    private void OnUIGameEnd()
    {
        mPlayScreen.SetActive(false);
        mEndScreen.SetActive(true);
    }
}
