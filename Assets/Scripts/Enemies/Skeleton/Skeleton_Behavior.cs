using UnityEngine;

public class Skeleton_Behavior : MonoBehaviour
{
    public bool resetPosition;
    public Level_Bounds_Behavior bounds;
    private Vector3 fullPos;

    void Update()
    {
        fullPos = transform.localPosition;
        if(resetPosition){
            //print("Resetting");  
            transform.localPosition = new Vector3(bounds.localScale.x - 0.5f, fullPos.y, fullPos.z);
        }
    }
}