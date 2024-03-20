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
    public bool isGrounded;
    public float jumpForce, duration, fallCheck = 10f;

    private Vector3 currentPosition;
    private Vector2 pos;


    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        DOTween.Init();
        jumpForce = 5f;
        duration = 1.5f;
        animator.SetBool("Moving", true);
        animator.SetBool("Falling", false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded && !animator.GetBool("Rolling")){
            pos = getPos();
            fallCheck = jumpForce + pos.y - 0.25f;
            print(jumpForce + pos.y);
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
        animator.SetBool("Jumping", false);
        animator.SetBool("Falling", false);
        isGrounded = true;
    }

    void OnTriggerExit2D(Collider2D feet)
    {
        if(!feet.CompareTag("Floor")){
            return;
        }
        isGrounded = false;
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
            transform.DOJump(new Vector2(x, y), force, 1, duration, false);
            animator.SetBool("Jumping", true);
        }
        // if(isGrounded == true){
        //     isJumping = true;
        //     rb.velocity = new Vector2(rb.velocity.x, force * Time.deltaTime);
        //     jumpTimeCounter = jumpTime;
        // }
        // if(Input.GetKeyUp("space") && isGrounded == false){
        //     animator.SetBool("Falling", true);
        //     isJumping = false;
        // }
        // if(Input.GetKey("space") && isGrounded == false && isJumping == true){
        //     if(jumpTimeCounter > 0){
        //         rb.velocity = new Vector2(rb.velocity.x, force * Time.deltaTime);
        //         jumpTimeCounter -= Time.deltaTime;
        //         animator.SetBool("Falling", true);
        //     }
        //     else{
        //         isJumping = false;
        //     }
        // }
    }
}
