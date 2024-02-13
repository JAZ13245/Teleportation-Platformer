using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Checkpoint : MonoBehaviour
{
    //gets the instance of Checkpoint present in the scene
    public static Checkpoint Instance {get; private set;}

    //player info
    private GameObject player;
    public Vector3 startPos = new Vector3(-32.45f, -3.75f, 0f);//again another magic number and im sorry
    public Vector3 savedPos;
    private float playerRadius = 0.8218632f;//this is a bandaid magic number sorry

    //checkpoint info
    private GameObject[] checkpoints;
    private int checkpointRadius = 1;//also a bandaid

    private void Awake()
    {
        //gets rid of any instances that aren't this one
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else//sets this instance as the current one and prevents it from being destroyed in the future
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        //checks if there is a player and saves if so
        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");

            //checks if there is not a saved position for the player and saves
            if (savedPos == Vector3.zero)
            {
                savedPos = startPos;
            }
            else if(savedPos != startPos)//moves player to last non-start position
            {
                player.transform.position = savedPos;
            }
        }

        //checks for checkpoints and saves if there
        if(GameObject.FindGameObjectsWithTag("Checkpoint") != null)
        {
            checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(checkpoints != null)//checks if there are checkpoints
        {
            //runs through all the checkpoints
            foreach(GameObject c in checkpoints)
            {
                //checks for collisions and runs the checkpoint function if so
                if (Collision(c))
                {
                    onCheckPoint(c);
                }
            }
        }
    }

    public void onCheckPoint(GameObject c)
    {
        //checks if this checkpoint has been reached yet and makes it green + records position if not
        //this is also super bandaid-y and will be fixed
        if(c.GetComponent<SpriteRenderer>().color != Color.green)
        {
            c.GetComponent<SpriteRenderer>().color = Color.green;
            savedPos = new Vector3(c.transform.position.x, c.transform.position.y, 0f);
        }
    }

    //checks if player collided with a given checkpoint
    private bool Collision(GameObject c)
    {
        //gets the centers of both
        Vector3 pCenter = player.GetComponentInChildren<SpriteRenderer>().bounds.center;
        Vector3 cCenter = c.GetComponent<SpriteRenderer>().bounds.center;

        //determines the distance between the centers of the player and the given checkpoint
        float distance = (float)Math.Sqrt(Math.Pow(cCenter.x - pCenter.x, 2) + Math.Pow(cCenter.z - pCenter.z, 2));

        //checks if there is a collision, returns true if true
        if (distance < checkpointRadius + playerRadius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //this function is bad and I (Carrie) will change it once we have enemy prefabs
    //reloads the scene by literally loading the scene
    public void ReloadScene()
    {
        //clean up
        player = null;
        checkpoints = null;

        SceneManager.LoadScene(4, LoadSceneMode.Single);//loads temp testing scene

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");

            if (savedPos != startPos)//moves player to last non-start position
            {
                player.transform.position = savedPos;
            }
        }
    }
}
