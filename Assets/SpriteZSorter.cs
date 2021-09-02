using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteZSorter : MonoBehaviour //Adjusts sprites z order dynamically
{
    public bool always_on_Top=false; //Precedence over behind
    public bool always_Behind=false;
    public bool is_Player=false;

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
        
        if (always_on_Top)
            sr.sortingOrder=short.MaxValue;
        else {
            if (always_Behind)
                sr.sortingOrder=short.MinValue;
            else {
                if (is_Player && coll) { //Sort by collider if it's player
                    sr.sortingOrder = (int)(coll.bounds.min.y*-10f); 
                }
                else //Otherwise by sprite bounds
                    sr.sortingOrder = (int)(sr.bounds.min.y*-10f); 
            }
        }
    }
}
