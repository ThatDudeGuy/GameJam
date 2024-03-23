using Unity.VisualScripting;
using UnityEngine;

public class Level_Bounds_Behavior : MonoBehaviour
{
    private Floor_Behavior under_Floor;
    public Vector3 localPosition;
    public Vector3 localScale;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Underworld_Foreground_LEFT") || other.gameObject.CompareTag("Underworld_Foreground_RIGHT")
        || other.gameObject.CompareTag("Underworld_Background_LEFT") || other.gameObject.CompareTag("Underworld_Background_RIGHT") 
        || other.gameObject.CompareTag("Overworld_Foreground_LEFT") || other.gameObject.CompareTag("Overworld_Foreground_RIGHT")
        || other.gameObject.CompareTag("Overworld_Background_LEFT") || other.gameObject.CompareTag("Overworld_Background_RIGHT")
        || other.gameObject.CompareTag("middle_Background_Overworld_LEFT") || other.gameObject.CompareTag("middle_Background_Overworld_RIGHT")
        || other.gameObject.CompareTag("far_Background_Overworld_LEFT") || other.gameObject.CompareTag("far_Background_Overworld_RIGHT")
        || other.gameObject.CompareTag("rocks_LEFT") || other.gameObject.CompareTag("rocks_RIGHT")){
            under_Floor = other.gameObject.GetComponent<Floor_Behavior>();
            if (under_Floor != null) {
                under_Floor.resetPosition = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other){
        //|| other.name == "floor_1_overworld" || other.name == "floor_2_overworld"
        if(other.gameObject.CompareTag("Underworld_Foreground_LEFT") || other.gameObject.CompareTag("Underworld_Foreground_RIGHT")
        || other.gameObject.CompareTag("Underworld_Background_LEFT") || other.gameObject.CompareTag("Underworld_Background_RIGHT") 
        || other.gameObject.CompareTag("Overworld_Foreground_LEFT") || other.gameObject.CompareTag("Overworld_Foreground_RIGHT")
        || other.gameObject.CompareTag("Overworld_Background_LEFT") || other.gameObject.CompareTag("Overworld_Background_RIGHT")
        || other.gameObject.CompareTag("middle_Background_Overworld_LEFT") || other.gameObject.CompareTag("middle_Background_Overworld_RIGHT")
        || other.gameObject.CompareTag("far_Background_Overworld_LEFT") || other.gameObject.CompareTag("far_Background_Overworld_RIGHT")
        || other.gameObject.CompareTag("rocks_LEFT") || other.gameObject.CompareTag("rocks_RIGHT")){
            under_Floor = other.gameObject.GetComponent<Floor_Behavior>();
            if (under_Floor != null) {
                under_Floor.resetPosition = true;
            }
        }
        if(other.CompareTag("Skeleton") 
        || other.CompareTag("Mushroom")
        || other.CompareTag("Flying_Eye")
        || other.CompareTag("Goblin")
        || other.CompareTag("Huntress_Spear")
        || other.CompareTag("Huntress_Bow")
        || other.CompareTag("Knight_No_Helmet")
        || other.CompareTag("Platform")
        || other.gameObject.name.Contains("Projectile")){
            Destroy(other.gameObject);
        }
    }
}