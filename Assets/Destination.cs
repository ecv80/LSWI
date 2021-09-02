using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    public string sceneName="";
    public bool ignorePosition=true;
    public Vector3 position=Vector3.zero;
    public Facing facing=Facing.Left;
}
