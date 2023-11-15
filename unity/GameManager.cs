using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void OnStartGame(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
        Debug.Log("Button Clicked for SceneName: " + SceneName);
    }
}
