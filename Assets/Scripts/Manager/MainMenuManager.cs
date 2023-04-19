using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(TagManager.CHOSE_PLAYER_SCENE_NAME);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
