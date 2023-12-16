using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreenHandler : MonoBehaviour
{
    [SerializeField]
    GameObject _playerReference;

    [SerializeField]
    GameObject _pauseScreen;

    public bool IsPaused = false;

    void Start()
    {
        _playerReference = GameObject.FindGameObjectWithTag("Player");
        _pauseScreen?.SetActive(false);
    }

    void Update()
    {
        PauseGame();
    }

    public void PauseGame()
    {
        _playerReference.GetComponent<PlayerController>().PauseEvent += OnPause;
    }

    public void OnPause()
    {
        IsPaused = !IsPaused;

        if (IsPaused)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            _pauseScreen.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _pauseScreen?.SetActive(false);
        }
    }

    public void RestartLevel()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        Time.timeScale = 1f;

        SceneManager.LoadScene(currentScene);
    }

    public void QuitGame()
    {
        Debug.Log("Qutting Game");
        Application.Quit();
    }
}
