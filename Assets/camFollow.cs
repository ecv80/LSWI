using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camFollow : MonoBehaviour
{
    float z;
    Camera cam; //Because I think it might be faster than Camera.main

    // Start is called before the first frame update
    void Start()
    {
        z=transform.position.z;
        cam=GetComponent<Camera>();
    }

    void LateUpdate()
    {   
        float dT=Time.deltaTime;

        Player p=GameManager.instance.player;
        transform.Translate((p.transform.position-transform.position)*dT*p.getSpeed()*0.4f);
        transform.position=new Vector3(transform.position.x, transform.position.y, z);


    }
}
