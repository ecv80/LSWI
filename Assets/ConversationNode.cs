using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationNode
{//I know, I know... all public is not proper OOP but I'm alone
    public string NPCText="";
    public string PlayerReply1="";
    public string PlayerReply2="";
    public string PlayerReply3="";
    public ConversationNode PlayerReply1NextNode=null;
    public ConversationNode PlayerReply2NextNode=null;
    public ConversationNode PlayerReply3NextNode=null;

    public ConversationNode(string[] strings) {
        if (strings.Length<2)
            throw new System.Exception("Conversations must at least have 1 statement and 1 reply");
        NPCText=strings[0];
        PlayerReply1=strings[1];
        PlayerReply2=strings.Length>2?strings[2]:"";
        PlayerReply3=strings.Length>3?strings[3]:"";
    }

    public ConversationNode setNextNode (int forReply, ConversationNode node) {
        if (forReply<1 || forReply>3)
            throw new System.Exception("Specify a reply from 1 to 3 to bind to a next node");
        switch(forReply) {
            case 1: PlayerReply1NextNode=node;
            break;
            case 2: PlayerReply2NextNode=node;
            break;
            case 3: PlayerReply3NextNode=node;
            break;
            default:
            break;
        }

        return this; //So we could chain the calls after each other for ease
    }
}
