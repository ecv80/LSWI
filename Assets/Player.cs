using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float speed;
    CapsuleCollider2D coll;
    Collider2D[] collidedColliders=new Collider2D[10]; //Hopefully we won't collide with more than 10 colliders at once!
    ContactFilter2D contactFilter2D=new ContactFilter2D().NoFilter();

    class DebugPoint {
        public Vector3 point;
        public Color color;

        public DebugPoint(Vector3 p, Color c) {
            point=p;
            color=c;
        }
    }

    List<DebugPoint> debugPoints=new List<DebugPoint>();

    // Start is called before the first frame update
    void Start()
    {
        speed=transform.localScale.x*20f;
        coll=GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dT=Time.deltaTime;

        //Input (and movement for now)
        if (Input.GetKey(KeyCode.LeftArrow)||Input.GetKey(KeyCode.A))
            transform.position=transform.position+Vector3.left*dT*speed;
        if (Input.GetKey(KeyCode.RightArrow)||Input.GetKey(KeyCode.D))
            transform.position=transform.position+Vector3.right*dT*speed;
        if (Input.GetKey(KeyCode.UpArrow)||Input.GetKey(KeyCode.W))
            transform.position=transform.position+Vector3.up*dT*speed;
        if (Input.GetKey(KeyCode.DownArrow)||Input.GetKey(KeyCode.S))
            transform.position=transform.position+Vector3.down*dT*speed;

        //Collisions
        Physics2D.SyncTransforms();

        //This code vvvvvvvvvvvvvvvvvvvvvvvv is from this tutorial: 
        //https://roystan.net/articles/character-controller-2d.html
        //with a few changes and I will be syncing the transforms manually instead

        Physics2D.OverlapCapsule(coll.bounds.center, coll.size, coll.direction, 0, contactFilter2D, collidedColliders);

        foreach (Collider2D hit in collidedColliders) {
            if (hit==null)
                continue;
            if (hit == coll)
                continue;

            //debugPoints.Add(new DebugPoint(hit.bounds.center, Color.red));

            ColliderDistance2D colliderDistance = hit.Distance(coll);

            if (colliderDistance.isOverlapped)
                transform.Translate((colliderDistance.pointA - colliderDistance.pointB));
        }
        
        //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
    }

    void OnDrawGizmos() //Debugging
    {
        foreach (DebugPoint dp in debugPoints) {
            Gizmos.color = dp.color;
            Gizmos.DrawSphere(dp.point, .1f);
        }
        
        //We don't want this to take forever processing all history, so discard old stuff
        if (debugPoints.Count>99) {
            debugPoints.RemoveRange(0, debugPoints.Count-100);
        }
    }
}
