using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarScr : InteractableObj
{
    public ParticleSystem ps;
    bool play = true;

    public override void interact()
    {
        play = !play;

        if (play) ps.Play();
        else ps.Stop();
    }
}
