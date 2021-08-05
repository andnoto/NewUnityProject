// To work, you must set Edit - Project Setting - Time - Fixed Timestep 0.15

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionRequired : PhysicsObject
{
    public float takeOffSpeed = 7;
    public float parametricSpeed = 7;
    
    //
    public float jumpRate = 5.0f;
    protected bool jump;
    private float timeStartJump;
    //
    protected bool rightMove;
    protected bool leftMove;
    //
    private SpriteRenderer spriteRenderer;
    //
 
    //
    // Use this for initialization
    void Awake()
    {
        spriteRenderer = newSprite.GetComponent<SpriteRenderer>();
    }
    protected override void ComputeMovement()
    {
        Vector2 moveNewSprite = Vector2.zero;
        // 
        // moveNewSprite.x = Input.GetAxis("Horizontal");
        //
        // move left or right 
        if (rightMove)
        {
            moveNewSprite.x = 1.0f;
            rightMove = false;
        }
        else
               if (leftMove)
        {
            moveNewSprite.x = -1.0f;
            leftMove = false;
        }
        ultimateSpeed = moveNewSprite * parametricSpeed;
        //
        // 
        bool reverseSprite;
        if (spriteRenderer.flipX)
            { if (moveNewSprite.x > 0.01f)
                reverseSprite = true;
                else
                reverseSprite = false;
            }
            else
            { if (moveNewSprite.x < 0.01f)
                reverseSprite = true;
                else
                reverseSprite = false;
            }
        // the previous statements can be summarized as follows (condition? consequent: alternative)
        // bool reverseSprite = (spriteRenderer.flipX ? (moveNewSprite.x > 0.01f) : (moveNewSprite.x < 0.01f));
        if (reverseSprite)
        {
            // spin the sprite in case of change of direction
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
        // after a few seconds it stops jumping
        if (jump)
        {
            float timeJump = Time.time - timeStartJump;
            if (timeJump > jumpRate)
                jump = false;
        }
        if (jump && landed)
        {
            speed.y = takeOffSpeed;
        }
        else if (!jump)
        {
            if (speed.y > 0)
            {
                // after each fixed update call it halves the speed of any jump
                speed.y = speed.y * 0.5f;
            }
        }

 
    }
    // Execute action requested By the result of speech recognition.
    public override void StartAction(string[] words)
    {
        // AZIONE
        Vector3 scale = newSprite.transform.localScale;
        string grande = "grande";
        string piccolo = "piccolo";
        string salta = "salta";
        string destra = "destra";
        string sinistra = "sinistra";
        jump = false;
        rightMove = false;
        leftMove = false;
#if UNITY_EDITOR
 //         jump = true;
            rightMove = true;    
 //           timeStartJump = Time.time;
#elif UNITY_ANDROID
        foreach (string parola in words)
        {
            if (parola == grande)
            {
                scale.x = scale.x * 1.5f;
                scale.y = scale.y * 1.5f;
                newSprite.transform.localScale = scale;
                break;
            } 
            if (parola == piccolo)
                    {
                      scale.x = scale.x / 1.5f;
                      scale.y = scale.y / 1.5f;
                      newSprite.transform.localScale = scale;
                      break;
                    } 
            if (parola == destra)
                            {
                                rightMove = true;
                                break;
                            } 
            if (parola == sinistra)
                                    {
                                         leftMove = true;
                                         break;
                                    } 
            jump = true;
            timeStartJump = Time.time;
         }
#endif
        // log 
        //   if (logText != null) logText.text = "Action Required called jump=" + jump;
    }
    //
}

