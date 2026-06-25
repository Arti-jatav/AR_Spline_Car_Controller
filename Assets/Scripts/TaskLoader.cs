using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TaskLoader : MonoBehaviour
{
    [SerializeField] private Button task1Btn;
    [SerializeField] private Button task2Btn;

    private void Start()
    {
        task1Btn.onClick.AddListener(OnTask1ButtonClicked);
        task2Btn.onClick.AddListener(OnTask2ButtonClicked);
    }

    private void OnTask1ButtonClicked()
    {
        SceneManager.LoadScene("AR Car Spawn");
    }

    private void OnTask2ButtonClicked()
    {
        SceneManager.LoadScene("Car Path Movement");
    }
}
