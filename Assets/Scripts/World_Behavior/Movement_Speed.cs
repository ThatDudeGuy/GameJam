using UnityEngine;

public class Movement_Speed : MonoBehaviour
{
    private const float MOVE_SPEED = 3f;
    void Update()
    {
        transform.Translate(MOVE_SPEED * Time.deltaTime * Vector3.left);
    }
}
