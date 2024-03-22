using UnityEngine;

public class Movement_Speed : MonoBehaviour
{
    public const float MOVE_SPEED = 5f;
    void Update()
    {
        if(CompareTag("Platform")){
            transform.Translate(MOVE_SPEED/2 * Time.deltaTime * Vector3.left);
        }
        else{
            transform.Translate(MOVE_SPEED * Time.deltaTime * Vector3.left);
        }
    }
}
