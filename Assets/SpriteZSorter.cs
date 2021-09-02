using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteZSorter : MonoBehaviour //Adjusts sprites z order dynamically
{
    public bool alwaysOnTop=false; //Precedence over behind
    public bool alwaysBehind=false;
    public bool isPlayer=false;

    public enum Pivot {bottom, top, center};
    public Pivot pivot=Pivot.bottom;
    public float yThreshold=5f; //How far from the ref point (the pivot in the setting above) of the sprite we want to switch z order

    SpriteRenderer sr;
    Collider2D coll;

    // Start is called before the first frame update
    void Start()
    {
        sr=GetComponent<SpriteRenderer>();
        coll=GetComponent<Collider2D>();
    }

    void LateUpdate()
    {
        if (!sr || !sr.isVisible) //If sr is null or is not visible, return. (Legal statement because second condition is never checked unless sr is not null)
            return;
        
        if (alwaysOnTop)
            sr.sortingOrder=short.MaxValue;
        else {
            if (alwaysBehind)
                sr.sortingOrder=short.MinValue;
            else {
                if (isPlayer && coll) { //Sort by collider if it's player
                    sr.sortingOrder = (int)(coll.bounds.min.y*-10f); 
                }
                else {//Otherwise by sprite bounds
                    float y=pivot==Pivot.bottom?sr.bounds.min.y:
                        pivot==Pivot.center?sr.bounds.center.y:
                        pivot==Pivot.top?sr.bounds.max.y:0f;

                    sr.sortingOrder = (int)((y+yThreshold)*-10f); 
                }
            }
        }
    }
}
