using System;
using UnityEngine;

public class Floor_Behavior : MonoBehaviour
{
    public bool resetPosition;
    // private Level_Bounds_Behavior bounds;
    private Collider2D boxEdge;
    private GameObject lvl_Bounds;
    private void Start() {
        lvl_Bounds = GameObject.FindGameObjectWithTag("Bounds");
        boxEdge = lvl_Bounds.GetComponent<Collider2D>();
    }
    void Update()
    {
        if(resetPosition){
            transform.localPosition = new Vector3(boxEdge.bounds.max.x * 2, transform.localPosition.y, transform.localPosition.z);
        }
    }
}
