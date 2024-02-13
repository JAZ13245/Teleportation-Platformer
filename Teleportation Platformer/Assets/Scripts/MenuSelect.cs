using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This loads scenes from other places (was originally just for the menu scene)
/// </summary>
public class MenuSelect : MonoBehaviour
{
    public void LoadMain()
    {
        //to change the scene this loads go into build settings and figure out the number for the scene you want and put that here
        SceneManager.LoadScene(1, LoadSceneMode.Single);//currently loads sample scene
    }

    public void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    //returns to the menu scene
    public void LoadMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    //returns to the options scene
    public void LoadOptions()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    //goes to the pause scene after 'esc' is pressed
    public void OnPause()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }

    //for testing purposes this loads CarrieUITest scene
    public void LoadTemp()
    {
        SceneManager.LoadScene(4, LoadSceneMode.Single);
    }
}
