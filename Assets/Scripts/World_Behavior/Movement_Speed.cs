using UnityEngine;

public class Movement_Speed : MonoBehaviour
{
    public const float MOVE_SPEED = 5f;
    public float platformSpeed;
    void Update()
    {
        if(name.Contains("Platform")){
            transform.Translate(platformSpeed * Time.deltaTime * Vector3.left);
        }
        else if(CompareTag("Underworld_Background_LEFT") || CompareTag("Underworld_Background_RIGHT") 
                || CompareTag("Overworld_Background_LEFT") || CompareTag("Overworld_Background_RIGHT")){
            transform.Translate(MOVE_SPEED/3 * Time.deltaTime * Vector3.left);
        }
        else if(CompareTag("middle_Background_Overworld_LEFT") || CompareTag("middle_Background_Overworld_RIGHT")){
            transform.Translate(MOVE_SPEED/3.5f * Time.deltaTime * Vector3.left);
        }
        else if(CompareTag("far_Background_Overworld_LEFT") || CompareTag("far_Background_Overworld_RIGHT")){
            transform.Translate(MOVE_SPEED/5 * Time.deltaTime * Vector3.left);
        }
        else{
            transform.Translate(MOVE_SPEED * Time.deltaTime * Vector3.left);
        }
    }
}
