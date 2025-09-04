using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Player Movement Stuff
    public Rigidbody2D PlayerRB;
    public float speed;
    private Vector2 move = new Vector2();

    //Player sprites/animation
    public Animator PlayerAnim;
    private string currentAnim = "idleDown";
    public string currentFacing = "down";

    void Update()
    {
        //Move Player
        move.x = Input.GetAxis("Horizontal");
        move.y = Input.GetAxis("Vertical");
        PlayerRB.velocity = move * speed * Time.deltaTime;

        //Walk Animation
        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) { //Choose walk direction if moving
            if(Input.GetAxisRaw("Horizontal") < 0){
                PlayAnimation("walkLeft");
            } else if(Input.GetAxisRaw("Horizontal") > 0){
                PlayAnimation("walkRight");
            } else if(Input.GetAxisRaw("Vertical") > 0){
                PlayAnimation("walkUp");
            } else if(Input.GetAxisRaw("Vertical") < 0){
                PlayAnimation("walkDown");
            } 
        } else{ //Find direction to play idle animation in
            PlayAnimation("idle"+currentAnim.Substring(4));
            currentFacing = currentAnim.Substring(4).ToLower();
        }
    }

    public void PlayAnimation(string anim){
        PlayerAnim.ResetTrigger(currentAnim);
        PlayerAnim.SetTrigger(anim);
        currentAnim = anim;
    }
}
