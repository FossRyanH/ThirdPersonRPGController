using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int Score = 0;
    public int EnemyCount = 0;
    public int CollectiblesInLevel = 0;
    float _timer = 0;


    [SerializeField]
    public GameObject PlayerUI;
    [SerializeField]
    public GameObject ScoreAndTime;

    [SerializeField]
    GameObject _completeScreen;
    [SerializeField]
    GameObject _player;

    void Start()
    {
        EnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        PlayerUI = GameObject.FindGameObjectWithTag("PlayerHealthBar");
        _completeScreen = GameObject.FindGameObjectWithTag("Completed");
        _player = GameObject.FindGameObjectWithTag("Player");

        Score = 0;
        _timer = 0;
    }

    void Update()
    {
        CollectiblesInLevel = FindObjectsOfType<Collectible>().Length;
        SetItemCounter(CollectiblesInLevel);
        SetTimer(_timer);

        if (CollectiblesInLevel == 0)
        {
            _completeScreen.GetComponent<GameFinished>().CompleteLevel();
        }
        if (_player == null)
        {
            SetGameOver();
        }
    }

    public void SetScore(int incrementScoreValue)
    {
        Score += incrementScoreValue;
        ScoreAndTime.GetComponent<ScoreAndTime>().SetScoreText(Score);
    }

    public void SetItemCounter(int collectibles)
    {
        ScoreAndTime.GetComponent<ScoreAndTime>().SetCollectibleText(collectibles);
    }

    void SetTimer(float timer)
    {
        timer += Time.time;
        ScoreAndTime.GetComponent<ScoreAndTime>().SetTimerText(Mathf.Round(timer));
    }

    public void SetGameOver()
    {
        _completeScreen.GetComponent<GameFinished>().GameOver();
    }
}
