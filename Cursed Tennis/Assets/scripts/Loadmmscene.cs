using UnityEngine;
using UnityEngine.SceneManagement;

public class Loadmmscene : MonoBehaviour
{
    //public string sceneName; // Name of the scene you want to load

    public void LoadScene()
    {
        SceneManager.LoadScene("Main Menu");
        Debug.Log("laad nieuwe scene");
    }
}
