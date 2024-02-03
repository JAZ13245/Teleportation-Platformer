using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This loads the main scene from the menu screen, it might do more in the future
/// </summary>
public class MenuSelect : MonoBehaviour
{
    public void LoadMain()
    {
        //to change the scene this loads go into build settings and figure out the number for the scene you want and put that here
        SceneManager.LoadScene(1, LoadSceneMode.Single);//currently loads sample scene
    }
}
