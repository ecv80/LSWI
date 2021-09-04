using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public string greeting="Hello, I sell clothes!";

    ConversationNode firstNode=null;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize conversation data
        //This might be probably better done from JSON
        firstNode=new ConversationNode(new[] {"Hello, I sell clothes!", 
            "Oh, really? You don't say?",
            "Okay, show me something!",
            "Not interested."});
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startConversation() {
        GameManager.instance.showConversation(greeting, new[] {"Oh, really? You don't say?",
            "Okay, show me something!",
            "Not interested."});
    }
}
