using System;
using UnityEngine;

public class Floor_Behavior : MonoBehaviour
{
    public bool resetPosition;
    // private Level_Bounds_Behavior bounds;
    private Collider2D boxEdge, rightBound, leftBound, myCollider;
    private GameObject lvl_Bounds, rightBG, leftBG;
    private void Start() {
        //if the left floor has exited the lvlBound collision and the right max.x of its collider is greater than the max.x of the lvlBounds, resetPosition is false
        if(CompareTag("Overworld_Background_LEFT")){
            //get x coordinate of the collider of the right bg and reset position there
            rightBG = GameObject.FindGameObjectWithTag("Overworld_Background_RIGHT");
            rightBound = rightBG.GetComponent<Collider2D>();
        }
        else if(CompareTag("Overworld_Background_RIGHT")){
            //get x coordinate of the collider of the right bg and reset position there
            leftBG = GameObject.FindGameObjectWithTag("Overworld_Background_LEFT");
            leftBound = leftBG.GetComponent<Collider2D>();
        }
        else if(CompareTag("middle_Background_Overworld_LEFT")){
            //get x coordinate of the collider of the right bg and reset position there
            rightBG = GameObject.FindGameObjectWithTag("middle_Background_Overworld_RIGHT");
            rightBound = rightBG.GetComponent<Collider2D>();
        }
        else if(CompareTag("middle_Background_Overworld_RIGHT")){
            //get x coordinate of the collider of the right bg and reset position there
            leftBG = GameObject.FindGameObjectWithTag("middle_Background_Overworld_LEFT");
            leftBound = leftBG.GetComponent<Collider2D>();
        }
        else if(CompareTag("rocks_LEFT")){
            //get x coordinate of the collider of the right bg and reset position there
            rightBG = GameObject.FindGameObjectWithTag("rocks_RIGHT");
            rightBound = rightBG.GetComponent<Collider2D>();
        }
        else if(CompareTag("rocks_RIGHT")){
            //get x coordinate of the collider of the right bg and reset position there
            leftBG = GameObject.FindGameObjectWithTag("rocks_LEFT");
            leftBound = leftBG.GetComponent<Collider2D>();
        }
        else if(CompareTag("Underworld_Foreground_LEFT")){
            //get x coordinate of the collider of the right bg and reset position there
            rightBG = GameObject.FindGameObjectWithTag("Underworld_Foreground_RIGHT");
            rightBound = rightBG.GetComponent<Collider2D>();
        }
        else if(CompareTag("Underworld_Foreground_RIGHT")){
            //get x coordinate of the collider of the right bg and reset position there
            leftBG = GameObject.FindGameObjectWithTag("Underworld_Foreground_LEFT");
            leftBound = leftBG.GetComponent<Collider2D>();
        }
        else if(CompareTag("Underworld_Background_LEFT")){
            //get x coordinate of the collider of the right bg and reset position there
            rightBG = GameObject.FindGameObjectWithTag("Underworld_Foreground_RIGHT");
            rightBound = rightBG.GetComponent<Collider2D>();
        }
        else if(CompareTag("Underworld_Background_RIGHT")){
            //get x coordinate of the collider of the right bg and reset position there
            leftBG = GameObject.FindGameObjectWithTag("Underworld_Foreground_LEFT");
            leftBound = leftBG.GetComponent<Collider2D>();
        }
        else if(CompareTag("Overworld_Foreground_LEFT")){
            rightBG = GameObject.FindGameObjectWithTag("Overworld_Foreground_RIGHT");
            rightBound = rightBG.GetComponent<Collider2D>();
        }
        else if(CompareTag("Overworld_Foreground_RIGHT")){
            leftBG = GameObject.FindGameObjectWithTag("Overworld_Foreground_LEFT");
            leftBound = leftBG.GetComponent<Collider2D>();
        }
        lvl_Bounds = GameObject.FindGameObjectWithTag("Bounds");
        boxEdge = lvl_Bounds.GetComponent<Collider2D>();
        myCollider = GetComponent<Collider2D>();
    }
    void Update()
    {
        if(resetPosition){
            if(CompareTag("Overworld_Background_LEFT")){
                if(rightBound.bounds.max.x > boxEdge.bounds.max.x + 0.75f){
                    return;
                }
                else{
                    transform.localPosition = new Vector3(rightBound.bounds.max.x + (myCollider.bounds.size.x / 2f), transform.localPosition.y, transform.localPosition.z);
                }
            }
            else if(CompareTag("Overworld_Background_RIGHT")){
                if(leftBound.bounds.max.x > boxEdge.bounds.max.x + 0.75f){
                    return;
                }
                else{
                    transform.localPosition = new Vector3(leftBound.bounds.max.x + (myCollider.bounds.size.x / 2f), transform.localPosition.y, transform.localPosition.z);
                }
            }
            else if(CompareTag("middle_Background_Overworld_LEFT")){
                if(rightBound.bounds.max.x > boxEdge.bounds.max.x + 0.5f){
                    return;
                }
                else{
                    transform.localPosition = new Vector3(rightBound.bounds.max.x + (myCollider.bounds.size.x / 2f), transform.localPosition.y, transform.localPosition.z);
                }
            }
            else if(CompareTag("middle_Background_Overworld_RIGHT")){
                if(leftBound.bounds.max.x > boxEdge.bounds.max.x + 0.5f){
                    return;
                }
                else{
                    transform.localPosition = new Vector3(leftBound.bounds.max.x + (myCollider.bounds.size.x / 2f), transform.localPosition.y, transform.localPosition.z);
                }
            }
            else if(CompareTag("rocks_LEFT")){
                if(rightBound.bounds.max.x > boxEdge.bounds.max.x + 0.75f){
                    return;
                }
                else{
                    transform.localPosition = new Vector3(rightBound.bounds.max.x + (myCollider.bounds.size.x / 2f), transform.localPosition.y, transform.localPosition.z);
                }
            }
            else if(CompareTag("rocks_RIGHT")){
                if(leftBound.bounds.max.x > boxEdge.bounds.max.x + 0.75f){
                    return;
                }
                else{
                    transform.localPosition = new Vector3(leftBound.bounds.max.x + (myCollider.bounds.size.x / 2f), transform.localPosition.y, transform.localPosition.z);
                }
            }
            else if(CompareTag("Underworld_Background_LEFT")){
                if(rightBound.bounds.max.x > boxEdge.bounds.max.x + 1f){
                    return;
                }
                else{
                    transform.localPosition = new Vector3(rightBound.bounds.max.x + (myCollider.bounds.size.x / 2f), transform.localPosition.y, transform.localPosition.z);
                }
            }
            else if(CompareTag("Underworld_Background_RIGHT")){
                if(leftBound.bounds.max.x > boxEdge.bounds.max.x + 1f){
                    return;
                }
                else{
                    transform.localPosition = new Vector3(leftBound.bounds.max.x + (myCollider.bounds.size.x / 2f), transform.localPosition.y, transform.localPosition.z);
                }
            }
            else if(CompareTag("Underworld_Foreground_LEFT")){
                if(rightBound.bounds.max.x > boxEdge.bounds.max.x + 1f){
                    return;
                }
                else{
                    transform.localPosition = new Vector3(rightBound.bounds.max.x + (myCollider.bounds.size.x / 2f), transform.localPosition.y, transform.localPosition.z);
                }
            }
            else if(CompareTag("Underworld_Foreground_RIGHT")){
                if(leftBound.bounds.max.x > boxEdge.bounds.max.x + 1f){
                    return;
                }
                else{
                    transform.localPosition = new Vector3(leftBound.bounds.max.x + (myCollider.bounds.size.x / 2f), transform.localPosition.y, transform.localPosition.z);
                }
            }
            else if(CompareTag("Overworld_Foreground_LEFT")){
                if(rightBound.bounds.max.x > boxEdge.bounds.max.x + 0.5f){
                    return;
                }
                else{
                    transform.localPosition = new Vector3(rightBound.bounds.max.x + (myCollider.bounds.size.x / 2f), transform.localPosition.y, transform.localPosition.z);
                }
            }
            else if(CompareTag("Overworld_Foreground_RIGHT")){
                if(leftBound.bounds.max.x > boxEdge.bounds.max.x + 0.5f){
                    return;
                }
                else{
                    transform.localPosition = new Vector3(leftBound.bounds.max.x + (myCollider.bounds.size.x / 2f), transform.localPosition.y, transform.localPosition.z);
                }
            }
            // else{
            //     transform.localPosition = new Vector3(boxEdge.bounds.max.x + (myCollider.bounds.size.x / 2f), transform.localPosition.y, transform.localPosition.z);
            // }
            transform.localPosition = new Vector3(boxEdge.bounds.max.x + (myCollider.bounds.size.x / 2f), transform.localPosition.y, transform.localPosition.z);
        }
    }
}