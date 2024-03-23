using UnityEngine;

public class Projectile_Flying_Eye_Behaviour : MonoBehaviour
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
        transform.Translate(MOVE_SPEED_X * Time.deltaTime * Vector3.left);

        if(Player.transform.localPosition.y >= transform.localPosition.y && gameObject != null && Player.gameObject != null){
            transform.Translate(MOVE_SPEED_Y * Time.deltaTime * Vector3.up);
        }
        else if(Player.transform.localPosition.y <= transform.localPosition.y && gameObject != null && Player.gameObject != null){
            transform.Translate(MOVE_SPEED_Y * Time.deltaTime * Vector3.down);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            if(CompareTag("Projectile_Underworld")){
                animator.SetBool("Hit", true);
            }
        }
    }

    void myDestroy(){
        if(animator.GetBool("Hit")){
            Destroy(gameObject);
        }
    }
}
