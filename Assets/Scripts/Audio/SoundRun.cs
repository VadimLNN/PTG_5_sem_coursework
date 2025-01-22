using UnityEngine;

public class SoundRun : MonoBehaviour
{
    public AudioSource moveSound;

    void Update()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.15f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.15f)
        {
            if (moveSound.isPlaying) return;
            moveSound.Play();
        }
        else
        {
            moveSound.Stop();
        }
    }
}