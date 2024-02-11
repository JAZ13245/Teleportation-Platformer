using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //this function is bad and I (Carrie) will change it once we have enemy prefabs
    //reloads the scene by literally loading the scene
    public void ReloadScene()
    {
        SceneManager.LoadScene(4, LoadSceneMode.Single);//loads temp testing scene
    }
}
