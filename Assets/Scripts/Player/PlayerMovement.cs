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
    private bool isGrounded = true, isJumping;
    public float jumpForce, duration;

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
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            pos = getPos();
            jump(pos.x, pos.y, jumpForce);
        }
        
    }

    void OnTriggerEnter2D(Collider2D feet)
    {
        if(!feet.CompareTag("Floor")){
            return;
        }
        animator.SetBool("Jumping", false);
    }




    private Vector2 getPos(){
        currentPosition = transform.localPosition;
        Vector2 myPos = (Vector2)currentPosition;
        return myPos;
    }

    void jump(float x, float y, float force){
        if(!isGrounded){
            isJumping = true;
            animator.SetBool("Jumping", false);
        }
        else if(isGrounded){
            isJumping = false;
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
