using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadManager : MonoBehaviour
{
    [SerializeField] private GameObject[] levelsList;
    [SerializeField] private PathWaypoints[] playerLevelWaypoints;
    [SerializeField] private PlayerCarMovementController playerCarMovementController;

    private int currentLevelIndex = 0;

    private void Start()
    {
        playerCarMovementController.ResetPlayer(playerLevelWaypoints[currentLevelIndex]);
    }

    public void LoadNextLevel()
    {
        if (currentLevelIndex < 2)  // Because total 3 levels will be there
        {
            TrafficPoolManager activePool = levelsList[currentLevelIndex].GetComponentInChildren<TrafficPoolManager>();
            if (activePool != null)
            {
                activePool.ClearAndDestroyPool();
            }

            levelsList[currentLevelIndex].SetActive(false);
            currentLevelIndex++;
            levelsList[currentLevelIndex].SetActive(true);
            playerCarMovementController.ResetPlayer(playerLevelWaypoints[currentLevelIndex]);
        }
        else
        {
            SceneManager.LoadScene("Choose Task");
        }
    }
}