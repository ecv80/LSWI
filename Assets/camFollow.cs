using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camFollow : MonoBehaviour
{
    public Transform player;
    float z;

    // Start is called before the first frame update
    void Start()
    {
        z=transform.position.z;
    }

    void LateUpdate()
    {
        if (!player)
            return;

        transform.Translate(player.position-transform.position);
        transform.position=new Vector3(transform.position.x, transform.position.y, z);

    }
}
