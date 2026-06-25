using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private UIManager uiManager;

    private int score;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        uiManager.UpdateScoreDisplay(score);
    }

    public void TriggerGameOver()
    {
        uiManager.ShowFailScreen();
    }

    public void TriggerLevelWin()
    {
        uiManager.ShowWinScreen();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextDifficulty()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}