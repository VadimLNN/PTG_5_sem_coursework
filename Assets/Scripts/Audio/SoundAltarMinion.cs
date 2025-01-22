using UnityEngine;

public class SoundAltarMinion : MonoBehaviour
{
    public AudioSource minionsound;

    public void PlaySoundMinion ()
    {
        if (minionsound.isPlaying) return;
        minionsound.Play();
    }

}