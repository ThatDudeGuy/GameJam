using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;

public class All_Enemy_Behavior : MonoBehaviour
{
   private const float SKELL_SPEED = 2.5f;
   private const float MUSH_SPEED = 1.5f;
   private const float FLY_EYE_SPEED = 4.5f;
   private const float GOBLIN_SPEED = 5f;
   private Vector3 meleeDistance, rangeDistance, meleeCheck, rangeCheck;
   private Animator enemy_Animator, playerAnimator;
   private GameObject Player;
   private const float PLAYER_X = PlayerMovement.X_POS;
    void Start(){
        enemy_Animator = GetComponent<Animator>();
        Player = GameObject.Find("Player");
        playerAnimator = Player.GetComponent<Animator>();
        meleeDistance = new Vector3(2,0,0);
        rangeDistance = new Vector3(13,0,0);
        if(CompareTag("Skeleton") || CompareTag("Goblin")){
            enemy_Animator.SetBool("Moving", true);
        }
    }
    private void Awake() {
        enemy_Animator = GetComponent<Animator>();
        int randomNumber = Random.Range(0, 2);//Evaluates expression below, if it is true, the boolean is true, if it is false, the booloean is false
        //int randomNumber = 0;
        if(CompareTag("Mushroom") || CompareTag("Huntress_Spear")){
            enemy_Animator.SetBool("Moving", randomNumber == 1);//<------
        }
        
    }
    void Update()
    {
        if(enemy_Animator.GetBool("Moving")){
            meleeCheck = transform.localPosition - meleeDistance;
            if(PLAYER_X >= meleeCheck.x && PLAYER_X < transform.localPosition.x){
                enemy_Animator.SetBool("Attacking", true);
            }
        }
        else{
            rangeCheck = transform.localPosition - rangeDistance;
            if(PLAYER_X >= rangeCheck.x && PLAYER_X < transform.localPosition.x){
                enemy_Animator.SetBool("Range", true);
            }
        }

        if(CompareTag("Skeleton")) {
            transform.Translate(SKELL_SPEED * Time.deltaTime * Vector3.left);
        }

        if(CompareTag("Mushroom")) {
            if(!enemy_Animator.GetBool("Moving")){
                transform.Translate(Movement_Speed.MOVE_SPEED/2 * Time.deltaTime * Vector3.left);
            }
            else{
                transform.Translate(MUSH_SPEED * Time.deltaTime * Vector3.left);
            }
        }
        if(CompareTag("Flying_Eye")) {
            transform.Translate(FLY_EYE_SPEED * Time.deltaTime * Vector3.left);
        }
        if(CompareTag("Goblin")) {
            transform.Translate(GOBLIN_SPEED * Time.deltaTime * Vector3.left);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        //print(other);
        if(other.CompareTag("Player")){
            if(CompareTag("Skeleton") && playerAnimator.GetBool("Attacking")){
                enemy_Animator.SetBool("Death", true);
            }
            if(CompareTag("Mushroom") && playerAnimator.GetBool("Attacking")){
                enemy_Animator.SetBool("Death", true);
            }
            if(CompareTag("Goblin") && playerAnimator.GetBool("Attacking")){
                enemy_Animator.SetBool("Death", true);
            }
            if(CompareTag("Flying_Eye") && playerAnimator.GetBool("Attacking")){
                enemy_Animator.SetBool("Death", true);
            }
        }
    }
}