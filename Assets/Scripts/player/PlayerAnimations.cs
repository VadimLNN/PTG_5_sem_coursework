using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimations : MonoBehaviour
{
    Animator anim;
    bool onGround = true;

    void Start() => anim = GetComponent<Animator>();

    public void setAnimatorParameters(float x, float z)
    {
        anim.SetFloat("speed_x", x);
        anim.SetFloat("speed_z", z);
    }

    public void setOnGround(bool state)
    {
        //onGround = state;
        anim.SetBool("onGround", state);
    }

    public void jump()
    {
        setOnGround(false);
        anim.SetTrigger("jump");
    }

    void landing()
    {
        anim.ResetTrigger("jump");
    }

    public void setOrder(int order)
    {
        anim.SetInteger("order", order);
    }

    public void interact()
    {
        anim.SetTrigger("interaction");
    }

    public void Attack()
    {
        anim.SetTrigger("attack");
    }

    public void setBlock(bool state)
    {
        anim.SetBool("block", state);
    }

    public void stopInteraction()
    {
        anim.ResetTrigger("interaction");
    }
    public void stopAttack()
    {
        anim.ResetTrigger("attack");
    }

    public void SetDeath()
    {
        anim.SetTrigger("death");
    }
}
