using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScr : InteractableObj
{
    public Animator anim;
    bool isOpen = false;

    public override void interact()
    {
        isOpen = !isOpen;
        anim.SetBool("isOpen", isOpen);
    }
}
