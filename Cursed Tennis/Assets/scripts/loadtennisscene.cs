using UnityEngine;
using UnityEngine.SceneManagement;

public class Loadtennisscene : MonoBehaviour
{
    //public string sceneName; // Name of the scene you want to load

    public void LoadScene()
    {
        SceneManager.LoadScene("Test-Jordy");
        Debug.Log("laad nieuwe scene");
    }

    public void Quitgame()
        {
        Application.Quit();
        Debug.Log("quit game");
        }
    }
