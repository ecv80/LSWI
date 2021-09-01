using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSorter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void LateUpdate()
    {
        //Get all visible sprites first
        SpriteRenderer[] srs;
        srs=FindObjectsOfType<SpriteRenderer>(); //I know find functions are slow, but it's either this or attaching a script to all SpriteRenderer game objects, or maybe subclassing SpriteRenderer... Last one might be a good optimisation
        foreach(SpriteRenderer sr in srs) {
            sr.sortingOrder = (int)Camera.main.WorldToScreenPoint (sr.GetComponent<Collider2D>().bounds.min).y * -1;
        }
            
        
    }
}
