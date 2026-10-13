using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public string firstScene;
    public void PlayGame()
    {
        SceneManager.LoadScene(firstScene);
    }

    public void ExitGame(){
        Application.Quit();
    }

    public void OpenGallery(){
        //To-Do: Open gallery
    }

    void CloseSth(){
        
    }
}
