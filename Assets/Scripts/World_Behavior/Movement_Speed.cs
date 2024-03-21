using UnityEngine;

public class Movement_Speed : MonoBehaviour
{
    private const float MOVE_SPEED = 5f;
    void Update()
    {
        transform.Translate(MOVE_SPEED * Time.deltaTime * Vector3.left);
    }
}
