using UnityEngine;

public class Projectile_Behaviour : MonoBehaviour
{
    private const float MOVE_SPEED_X = 3.75f;
    private const float MOVE_SPEED_Y = 1f;
    private PlayerMovement Player;
    private Animator animator;
    private void Start() {
        Player = FindObjectOfType<PlayerMovement>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if(CompareTag("Flying_Eye_shot")){
            transform.Translate(MOVE_SPEED_X * 2 * Time.deltaTime * Vector3.left);
        }
        else{
            transform.Translate(MOVE_SPEED_X * Time.deltaTime * Vector3.left);
        }

        if(Player.transform.localPosition.y >= transform.localPosition.y && gameObject != null && Player.gameObject != null){
            transform.Translate(MOVE_SPEED_Y * Time.deltaTime * Vector3.up);
        }
        else if(Player.transform.localPosition.y <= transform.localPosition.y && gameObject != null && Player.gameObject != null){
            transform.Translate(MOVE_SPEED_Y * Time.deltaTime * Vector3.down);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            if(name.Contains("Projectile")){
                foreach (AnimatorControllerParameter parameter in animator.parameters)
                {
                    if (parameter.type == AnimatorControllerParameterType.Bool && parameter.name == "Hit")
                    {
                        animator.SetBool("Hit", true);
                    }
                    else{
                        Destroy(gameObject);
                    }
                }
            }
        }
    }

    void myDestroy(){ 
        if(animator.GetBool("Hit")){
            Destroy(gameObject);
        }        
    }
}