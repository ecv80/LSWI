using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Singleton. Part of this is copied from a previous project of mine vvvv

    public static GameManager instance=null;

    void Awake() { 
        if (!instance)
            instance=this;
        else
            throw new System.Exception("Attempted to reinstantiate GameManager");
        
        player=FindObjectOfType<Player>();
    }
    //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
