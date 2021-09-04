using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Facing {Left, Right, Up, Down};

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

        if (GameManager.instance.startInSetPosition) {
            transform.position=GameManager.instance.startingPosition;
            Face(GameManager.instance.startingFacing);
        }

        GameManager.instance.cam.FocusOn(transform.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dT=Time.deltaTime;

        //Input (and movement for now)
        if (Input.GetKey(KeyCode.LeftArrow)||Input.GetKey(KeyCode.A)) {
            Face(Facing.Left);
            transform.position=transform.position+Vector3.left*dT*speed;
        }
        if (Input.GetKey(KeyCode.RightArrow)||Input.GetKey(KeyCode.D)) {
            Face(Facing.Right);
            transform.position=transform.position+Vector3.right*dT*speed;
        }
        if (Input.GetKey(KeyCode.UpArrow)||Input.GetKey(KeyCode.W))
            transform.position=transform.position+Vector3.up*dT*speed;
        if (Input.GetKey(KeyCode.DownArrow)||Input.GetKey(KeyCode.S))
            transform.position=transform.position+Vector3.down*dT*speed;

        //Collisions
        Physics2D.SyncTransforms();

        //This code vvvvvvvvvvvvvvvvvvvvvvvv is from this tutorial: 
        //https://roystan.net/articles/character-controller-2d.html
        //with a few changes and I will be syncing the transforms manually instead

        int hits=Physics2D.OverlapCapsule(coll.bounds.center, coll.size, coll.direction, 0, contactFilter2D, collidedColliders);

        for (int i=0; i<hits; i++) {
            if (collidedColliders[i] == coll)
                continue;
            
            if (collidedColliders[i].isTrigger) { //Treat a trigger like a trigger ¬L¬
                //NOTE: For some reason, as soon as the trigger checkbox is ticked
                //trigger colliders appear to become bigger in height when detected by OverlapCapsule
                //than they really are. The collider gizmo is still drawn as the original size tho.
                //It appears to be about three times as high.
                Destination d=collidedColliders[i].GetComponent<Destination>();
                NPC npc=collidedColliders[i].GetComponent<NPC>();
                if (d) { //A teleport!
                    if (d.ignorePosition) {
                        GameManager.instance.startInSetPosition=false;
                    }
                    else {
                        GameManager.instance.startInSetPosition=true;
                        GameManager.instance.startingPosition=d.position;
                        GameManager.instance.startingFacing=d.facing;
                    }
                    SceneManager.LoadScene(d.sceneName);
                }
                if (npc) { //NPC interaction
                    npc.startConversation();
                }
            }
            else { //Move out of the collider
                //debugPoints.Add(new DebugPoint(hit.bounds.center, Color.red));

                ColliderDistance2D colliderDistance = collidedColliders[i].Distance(coll);

                if (colliderDistance.isOverlapped)
                    transform.Translate((colliderDistance.pointA - colliderDistance.pointB));
            }
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

    public float GetSpeed() {
        return speed;
    }

    void Face(Facing f) {
        switch(f) {
            case Facing.Left:
                if (transform.localScale.x<0)
                    transform.localScale=new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                break;
            case Facing.Right:
                if (transform.localScale.x>=0)
                    transform.localScale=new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                break;
            default:
                break;
        }
    }
}
