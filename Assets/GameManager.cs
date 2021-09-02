using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player player;
    public bool startInSetPosition=false;
    public Vector3 startingPosition=Vector3.zero;
    public Facing startingFacing=Facing.Left;

    public Cam cam;

    //Singleton. Part of this is copied from a previous project of mine vvvv
    public static GameManager instance=null;

    void Awake() { 
        if (!instance) {
            instance=this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
            //throw new System.Exception("Attempted to reinstantiate GameManager");
            Destroy(gameObject);
    }
    //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player=FindObjectOfType<Player>();
        cam=FindObjectOfType<Cam>();

        cam.bounds=GameObject.Find("SceneBounds").GetComponent<Collider2D>().bounds;
    }

    
}
