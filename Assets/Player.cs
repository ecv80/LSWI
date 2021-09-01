using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed=transform.localScale.x*20f;
    }

    // Update is called once per frame
    void Update()
    {
        float dT=Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftArrow)||Input.GetKey(KeyCode.A))
            transform.position=transform.position+Vector3.left*dT*speed;
        if (Input.GetKey(KeyCode.RightArrow)||Input.GetKey(KeyCode.D))
            transform.position=transform.position+Vector3.right*dT*speed;
    }
}
