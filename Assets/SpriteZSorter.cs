using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteZSorter : MonoBehaviour //Adjusts sprites z order dynamically
{
    public bool always_on_Top=false; //Precedence over behind
    public bool always_behind=false;

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
        if (!coll)
            return;
        
        if (always_on_Top)
            sr.sortingOrder=short.MaxValue;
        else
            if (always_behind)
                sr.sortingOrder=short.MinValue;
            else {
                sr.sortingOrder = (int)(coll.bounds.min.y*-10f); 
            }
    }
}
