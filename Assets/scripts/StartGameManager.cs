using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameManager : MonoBehaviour
{
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");

    }

    public void Bonus()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Bonus");

    }
}
