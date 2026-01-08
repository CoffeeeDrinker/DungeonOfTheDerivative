using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Player Movement Stuff
    public Rigidbody2D PlayerRB;
    public float speed;
    public Vector3 move = new Vector3();
    private Vector3 prevMove;

    //Player sprites/animation
    public Animator PlayerAnim;
    public string currentAnim = "idleDown";
    public string currentFacing = "down";

    //Player name (this isn't being used in this script but I'm just holding it here)
    public string playerName = PlayerManager.playerName;

    void Update()
    {
        //Move Player
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
        move.Normalize();
        PlayerRB.velocity = move * speed * Time.deltaTime;

        //Walk Animation
        if (prevMove == null || prevMove != move)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            { //Choose walk direction if moving
                if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    PlayAnimation("walkLeft");
                }
                else if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    PlayAnimation("walkRight");
                }
                else if (Input.GetAxisRaw("Vertical") > 0)
                {
                    PlayAnimation("walkUp");
                }
                else if (Input.GetAxisRaw("Vertical") < 0)
                {
                    PlayAnimation("walkDown");
                }
            }
            else
            { //Find direction to play idle animation in
                PlayAnimation("idle" + currentAnim.Substring(4));
                currentFacing = currentAnim.Substring(4).ToLower();
            }
        }
        prevMove = move;
    }

    public void PlayAnimation(string anim){
        if(anim != currentAnim)
        {
            PlayerAnim.ResetTrigger(currentAnim);
            PlayerAnim.SetTrigger(anim);
            currentAnim = anim;
        }
    }
}
