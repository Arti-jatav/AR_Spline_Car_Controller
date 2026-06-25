using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBackToTaskSelectionScene : MonoBehaviour
{

    public void LoadTaskSelectionScene()
    {
        SceneManager.LoadScene(Constants.CHOOSE_TASK_SCENE_NAME);
    }
}
