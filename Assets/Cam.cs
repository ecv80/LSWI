using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cam : MonoBehaviour
{
    float z;
    public Bounds bounds;
    Camera cam;

    void Awake()
    {
        z=transform.position.z;
        cam=GetComponent<Camera>();
    }

    void LateUpdate()
    {   
        if (!GameManager.instance.player)
            return;
        float dT=Time.deltaTime;

        //Calculated every frame in case of zoom changes, etc
        //This is taken from https://forum.unity.com/threads/orthographic-camera-width.299243/#post-1974507 with minor modifications vvv
        float camHalfHeight = cam.orthographicSize;
        float camHalfWidth = cam.aspect * camHalfHeight;
        //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

        Player p=GameManager.instance.player;
        transform.Translate((p.transform.position-transform.position)*dT*p.GetSpeed()*0.4f);
        transform.position=new Vector3(transform.position.x, transform.position.y, z);

        //Adjust to fit within bounds
        float leftMost=transform.position.x-camHalfWidth;
        float rightMost=transform.position.x+camHalfWidth;
        float bottomMost=transform.position.y-camHalfHeight;
        float topMost=transform.position.y+camHalfHeight;
        if (leftMost<=bounds.min.x && rightMost>=bounds.max.x)
            //Center it horizontally
            transform.position=new Vector3(bounds.center.x, transform.position.y, transform.position.z);            
        else {
            if (leftMost<bounds.min.x)
                transform.position=new Vector3(bounds.min.x+camHalfWidth, transform.position.y, transform.position.z);
            if (rightMost>bounds.max.x)
                transform.position=new Vector3(bounds.max.x-camHalfWidth, transform.position.y, transform.position.z);
        }
        if (bottomMost<=bounds.min.y && topMost>=bounds.max.y)
            //Center it vertically
            transform.position=new Vector3(transform.position.x, bounds.center.y, transform.position.z);
        else {
            if (bottomMost<bounds.min.y)
                transform.position=new Vector3(transform.position.x, bounds.min.y+camHalfHeight, transform.position.z);
            if (topMost>bounds.max.y)
                transform.position=new Vector3(transform.position.x, bounds.max.y-camHalfHeight, transform.position.z);
        }

    }

    public void FocusOn(Vector3 v) { //Change focus immediately
        transform.position=v;
        transform.position=new Vector3(transform.position.x, transform.position.y, z);
    }

}
