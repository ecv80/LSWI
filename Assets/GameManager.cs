using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    //Initialization
    public TextMeshProUGUI NPCText;
    public TextMeshProUGUI[] playerReplies;

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

    public void showConversation (string NPCtext, string[] replies) {
        NPCText.text=NPCtext;
        for (int i=0; i<playerReplies.Length; i++)
            if (i<replies.Length){
                playerReplies[i].enabled=true;
                playerReplies[i].text=replies[i];
            }
            else
                playerReplies[i].enabled=false;
    }

    
}
