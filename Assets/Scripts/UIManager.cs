using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject failScreenUi;
    [SerializeField] private GameObject winScreenUi;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button nextLevelButton;

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            restartButton.onClick.AddListener(GameManager.Instance.RestartLevel);
            nextLevelButton.onClick.AddListener(GameManager.Instance.LoadNextDifficulty);
        }
    }

    public void UpdateScoreDisplay(int currentScore)
    {
        scoreText.text = currentScore.ToString();
    }

    public void ShowFailScreen()
    {
        failScreenUi.SetActive(true);
    }

    public void ShowWinScreen()
    {
        winScreenUi.SetActive(true);
    }
}