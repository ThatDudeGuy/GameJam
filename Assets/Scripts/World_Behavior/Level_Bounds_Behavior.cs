using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Level_Bounds_Behavior : MonoBehaviour
{
    public Floor_Behavior floor_1_overworld;
    public Floor_Behavior floor_2_overworld;
    public Floor_Behavior floor_1_underworld;
    public Floor_Behavior floor_2_underworld;
    public Vector3 localPosition;
    public Vector3 localScale;
    
    private void Start() {
        localPosition = transform.localPosition;
        localScale = transform.localScale;
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.name == "floor_1_overworld"){
            floor_1_overworld.resetPosition = false;
        }
        if(other.name == "floor_2_overworld"){
            floor_2_overworld.resetPosition = false;
        }
        if(other.name == "floor_1_underworld"){
            floor_1_underworld.resetPosition = false;
        }
        if(other.name == "floor_2_underworld"){
            floor_2_underworld.resetPosition = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        // print("Object " + other +" has left");
        // print("Goodbye");
        if(other.name == "floor_1_overworld"){
            floor_1_overworld.resetPosition = true;
        }
        if(other.name == "floor_2_overworld"){
            floor_2_overworld.resetPosition = true;
        }
        if(other.name == "floor_1_underworld"){
            floor_1_underworld.resetPosition = true;
        }
        if(other.name == "floor_2_underworld"){
            floor_2_underworld.resetPosition = true;
        }

        if(other.CompareTag("Skeleton")){
            Destroy(other.gameObject);
        }
    }
    
}
