using UnityEngine;

public class Mushroom_Behavior : MonoBehaviour
{
    // public bool resetPosition;
    // public Level_Bounds_Behavior bounds;
    // private Vector3 fullPos;
    // private const float SKELL_SPEED = 2.5f;

    public GameObject mush_projectile;
    Vector3 spawnPosition;

    void Update()
    {
        spawnPosition = transform.localPosition;
        // fullPos = transform.localPosition;
        // if(resetPosition){
        //     //print("Resetting");  
        //     transform.localPosition = new Vector3(bounds.localScale.x - 0.5f, fullPos.y, fullPos.z);
        // }
    }

    void spawn(){
        Instantiate(mush_projectile, spawnPosition, Quaternion.identity);
    }

//     public void move(){
//         transform.Translate(SKELL_SPEED * Time.deltaTime * Vector3.left);
//     }
}
