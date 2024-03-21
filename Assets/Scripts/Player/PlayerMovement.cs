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
    public bool isGrounded, isJumping;
    public float jumpForce, duration, fallCheck = 10f;

    private Vector3 currentPosition;
    private Vector2 pos;

    private Sequence myTween;
    //USE myTween.KILL on the jump animation whenever we collide with a floor object. Being the ground plane or platforms

    void Start()
    {
        Application.targetFrameRate = 60;
        DOTween.Init();
        jumpForce = 5f;
        duration = 1.5f;
        animator.SetBool("Moving", true);
        animator.SetBool("Falling", false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded && !animator.GetBool("Rolling")){
            pos = getPos();
            fallCheck = jumpForce + pos.y - 0.25f;
            //print(jumpForce + pos.y);
            jump(pos.x, pos.y, jumpForce);
        }
        if(Input.GetKeyDown(KeyCode.D) && !animator.GetBool("Jumping")){
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
        
    }

    void OnTriggerEnter2D(Collider2D feet)
    {
        if(!feet.CompareTag("Floor")){
            return;
        }
        print(feet + "ENTER");
        animator.SetBool("Jumping", false);
        animator.SetBool("Falling", false);
        isGrounded = true;
        isJumping = false;
    }

    void OnTriggerExit2D(Collider2D feet)
    {
        print(feet + "EXIT");
        if(!feet.CompareTag("Floor")){
            return;
        }
        if(isJumping){
            isGrounded = false;
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
    }
    void endRoll(){
        animator.SetBool("Rolling", false);
    }

    void jump(float x, float y, float force){
        if(!isGrounded){
            animator.SetBool("Jumping", false);
        }
        else if(isGrounded){
            myTween = transform.DOJump(new Vector2(x, y), force, 1, duration, false);
            animator.SetBool("Jumping", true);
            isJumping = true;
        }
    }
}
