using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class All_Enemy_Behavior : MonoBehaviour
{
    // Base speed should be ay least for projectiles 3.5f
   private const float PJ_MUSH_SPEED = 4f;
   private const float PJ_FLY_EYE_SPEED = 5.0f;
   private const float SKELL_SPEED = 2.5f;
   private const float MUSH_SPEED = 1.5f;
   private const float FLY_EYE_SPEED = 4.5f;
   private const float GOBLIN_SPEED = 5f;
   private Animator skell_Animator;
   private GameObject Player;
   private Animator playerAnimator;
//    public Animator mush_Animator;
//    public Animator fly_eye_Animator;
//    public Animator goblin_Animator;

    void Start(){
        skell_Animator = GetComponent<Animator>();
        Player = GameObject.Find("Player");
        playerAnimator = Player.GetComponent<Animator>();
    }
    void Update()
    {
        if(CompareTag("Skeleton")) {
            transform.Translate(SKELL_SPEED * Time.deltaTime * Vector3.left);
        }
        if(name == "Mushroom") {
            transform.Translate(MUSH_SPEED * Time.deltaTime * Vector3.left);
        }
        if(name == "Flying_Eye") {
            transform.Translate(FLY_EYE_SPEED * Time.deltaTime * Vector3.left);
        }
        if(name == "Goblin") {
            transform.Translate(GOBLIN_SPEED * Time.deltaTime * Vector3.left);
        }
        if(name == "Projectile_Flying_Eye") {
            transform.Translate(PJ_FLY_EYE_SPEED * Time.deltaTime * Vector3.left);
        }
        if(name == "Projectile_Mushroom") {
            transform.Translate(PJ_MUSH_SPEED * Time.deltaTime * Vector3.left);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        //print(other);
        if(other.CompareTag("Player")){
            //print("Player Hit Skeleton");
            if(CompareTag("Skeleton") && playerAnimator.GetBool("Attacking")){
                skell_Animator.SetBool("Death", true);
            }
            else if(CompareTag("Skeleton")){
                print("Attacking Player");
                skell_Animator.SetBool("Attacking", true);
            }
        }
    }

}