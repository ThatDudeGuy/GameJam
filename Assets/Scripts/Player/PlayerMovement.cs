using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using DG.Tweening;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public bool isGrounded, isJumping, dimensionSwitch = false;
    public float jumpForce, duration, fallCheck = 2f;
    private const float OVERWORLD_Y = 0.31f, X_POS = -0.075f;
    private const float UNDERWORLD_Y = -13.8f;
    private Vector3 currentPosition;
    private Vector2 pos;
    private Sequence myJumpTween;
    public Tweener  myFallTween;
    private GameObject[] all_Enemies;
    private Animator enemy_Animator;
    //USE myTween.KILL on the jump animation whenever we collide with a floor object. Being the ground plane or platforms
    void Start()
    {
        Application.targetFrameRate = 60;
        DOTween.Init();
        jumpForce = 5f;
        duration = 1.5f;
        animator.SetBool("Moving", true);
        animator.SetBool("Falling", false);
        animator.SetBool("Attacking", false);
        transform.localPosition = new Vector3(X_POS, OVERWORLD_Y, 0);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded && !isJumping && !animator.GetBool("Rolling")){
            pos = getPos();
            fallCheck = jumpForce + pos.y - 1f;
            //print(fallCheck);
            if(!dimensionSwitch){
                jump(pos.x, OVERWORLD_Y, jumpForce);
            }
            else{
                jump(pos.x, UNDERWORLD_Y, jumpForce);
            }
        }
        if(Input.GetKeyDown(KeyCode.D) && !animator.GetBool("Jumping")){
            dimensionSwitch = !dimensionSwitch;
            roll();
        }
        if(Input.GetMouseButtonDown(0)){
            attack();
        }
        currentPosition = transform.localPosition;
        if(currentPosition.y >= fallCheck){
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", true);
        }
        if(myFallTween != null){
            pos = getPos();
            fallCheck = jumpForce + pos.y - 1f;
            myFallTween.OnKill(() => {
                if(!dimensionSwitch){
                    transform.localPosition = new Vector3(X_POS, OVERWORLD_Y, 0);
                }
                else{
                    transform.localPosition = new Vector3(X_POS, UNDERWORLD_Y, 0);
                }
            });
        }
    }

    void OnTriggerEnter2D(Collider2D otherObject)
    {
        if(otherObject.CompareTag("Floor") || otherObject.CompareTag("Platform")){
            //print(otherObject + "ENTER");
            if(!animator.GetBool("Rolling")){
                myJumpTween.Kill();
                myFallTween.Kill();
            }
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", false);
            isGrounded = true;
            isJumping = false;
        }
        if(otherObject.CompareTag("Skeleton")){
            all_Enemies = GameObject.FindGameObjectsWithTag("Skeleton");
            print(all_Enemies);
            foreach (var item in all_Enemies)
            {
                enemy_Animator = item.GetComponent<Animator>();
                if(enemy_Animator.GetBool("Attacking")){
                    animator.SetBool("Hurt", true);
                    print("Player Has Been Struck");
                    return;
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D otherObject)
    {
        //print(otherObject + "EXIT");
        if(otherObject.CompareTag("Floor") || otherObject.CompareTag("Platform")){
            if(isJumping){
                isGrounded = false;
            }
        } 
        if(otherObject.CompareTag("Platform") && !isJumping){
            animator.SetBool("Falling", true);
            myFallTween = transform.DOMoveY(OVERWORLD_Y, 0.75f, false);
        }
        if(otherObject.CompareTag("Skeleton")){
            animator.SetBool("Hurt", false);
            all_Enemies = GameObject.FindGameObjectsWithTag("Skeleton");
            foreach (var item in all_Enemies)
            {
                enemy_Animator = item.GetComponent<Animator>();
                if(enemy_Animator.GetBool("Attacking")){
                    enemy_Animator.SetBool("Attacking", false);
                    return;
                }
            }
        }
    }

    private Vector2 getPos(){
        Vector3 currentPosition = transform.localPosition;
        Vector2 myPos = (Vector2)currentPosition;
        return myPos;
    }

    void attack(){
        animator.SetBool("Attacking", true);
    }
    void endAttack(){
        animator.SetBool("Attacking", false);
    }
    void roll(){
        animator.SetBool("Rolling", true);
        if(dimensionSwitch){
            myFallTween = transform.DOMoveY(UNDERWORLD_Y, 1, false);
        }
        else{
            myFallTween = transform.DOMoveY(OVERWORLD_Y, 1, false);
        }
        //myFallTween.onComplete();
    }
    void endRoll(){
        animator.SetBool("Rolling", false);
    }

    void jump(float x, float y, float force){
        if(!isGrounded){
            animator.SetBool("Jumping", false);
        }
        else if(isGrounded){
            myJumpTween = transform.DOJump(new Vector2(x, y), force, 1, duration, false);
            animator.SetBool("Jumping", true);
            isJumping = true;
        }
    }

}