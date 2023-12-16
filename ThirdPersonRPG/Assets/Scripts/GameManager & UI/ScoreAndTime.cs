using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreAndTime : MonoBehaviour
{
    [SerializeField]
    TMP_Text _scoreText;
    [SerializeField]
    TMP_Text _timerText;
    [SerializeField]
    TMP_Text _collectibleText;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void SetScoreText(int score)
    {
        _scoreText.text = score.ToString();
    }

    public void SetTimerText(float timer)
    {
        _timerText.text = timer.ToString();
    }

    public void SetCollectibleText(int remainingItems)
    {
        _collectibleText.text = remainingItems.ToString();
    }
}
