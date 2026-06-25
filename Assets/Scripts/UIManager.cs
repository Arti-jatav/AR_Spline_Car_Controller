using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System;

public class UIManager : MonoBehaviour
{
    public static Action OnNextLevelLoad;

    [SerializeField] private GameObject gameOverScreenUi;
    [SerializeField] private GameObject winScreenUi;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button nextLevelButton;

    private void Start()
    {
        ResetScore();
        restartButton.onClick.AddListener(OnResetButtonClicked);
        nextLevelButton.onClick.AddListener(OnNextLvlButtonClicked);
    }

    public void UpdateScoreDisplay(int currentScore)
    {
        scoreText.text = currentScore.ToString();
    }

    public void ShowGameOverScreen()
    {
        gameOverScreenUi.SetActive(true);
    }

    public void ShowWinScreen()
    {
        winScreenUi.SetActive(true);
    }

    public void ResetScore()
    {
        scoreText.text = "0";
    }

    private void OnResetButtonClicked()
    {
        GameManager.Instance.RestartLevel();
        ResetScore();
        gameOverScreenUi.SetActive(false);
    }

    private void OnNextLvlButtonClicked()
    {
        GameManager.Instance.LoadNextLevel();
        ResetScore();
        winScreenUi.SetActive(false);
        OnNextLevelLoad?.Invoke();
    }
}