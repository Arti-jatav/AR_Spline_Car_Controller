using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBackToTaskSelectionScene : MonoBehaviour
{
    public void LoadTaskSelectionScene()
    {
        SceneManager.LoadScene("Choose Task");
    }
}
