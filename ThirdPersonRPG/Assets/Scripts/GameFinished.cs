using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFinished : MonoBehaviour
{
    [SerializeField]
    GameObject _gameFinishedScreen;
    [SerializeField]
    GameObject _gameOverScreen;

    void Start()
    {
        _gameFinishedScreen.SetActive(false);
        _gameOverScreen.SetActive(false);
    }

    public void CompleteLevel()
    {
        _gameFinishedScreen.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void GameOver()
    {
        _gameOverScreen.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}
