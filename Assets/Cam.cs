using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cam : MonoBehaviour
{
    float z;

    void Awake()
    {
        z=transform.position.z;
    }

    void LateUpdate()
    {   
        if (!GameManager.instance.player)
            return;
        float dT=Time.deltaTime;

        Player p=GameManager.instance.player;
        transform.Translate((p.transform.position-transform.position)*dT*p.GetSpeed()*0.4f);
        transform.position=new Vector3(transform.position.x, transform.position.y, z);

    }

    public void FocusOn(Vector3 v) { //Change focus immediately
        transform.position=v;
        transform.position=new Vector3(transform.position.x, transform.position.y, z);
    }
}
