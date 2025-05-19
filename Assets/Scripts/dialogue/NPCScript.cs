using UnityEngine;

public class NPCScript : MonoBehaviour
{
    public TextAsset currentDialogue;
    public TextAsset nextDialog;
    public GameObject door;
    public GameObject dialogueSystem;

    public void interact()
    {
        dialogueSystem.GetComponent<DialogueSystem>().loadDialogue(currentDialogue);
        dialogueSystem.GetComponent<DialogueSystem>().setAction("door open", openDoor);
        dialogueSystem.GetComponent<DialogueSystem>().setAction("dialogue change", changeDlg);
        dialogueSystem.GetComponent<DialogueSystem>().setAction("make smarter", makeSmarter);
        dialogueSystem.GetComponent<DialogueSystem>().setAction("make stronger", makeStronger);
        dialogueSystem.GetComponent<DialogueSystem>().setAction("door strong open", kickIn);
        dialogueSystem.GetComponent<DialogueSystem>().setAction("door smart open", smartOpen);
        dialogueSystem.GetComponent<DialogueSystem>().setAction("door close", closeDoor);
    }

    public void openDoor()
    {
        Animator anim = door.GetComponent<Animator>();
        anim.SetBool("isOpen", true);
    }

    public void changeDlg()
    {
        currentDialogue = nextDialog;
    }

    public void makeSmarter()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().intellect += 1;
    }

    public void makeStronger()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().power += 1;
    }

    public void kickIn()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().power > 1)
        {
            Animator anim = door.GetComponent<Animator>();
            anim.SetTrigger("strong");
        }
    }

    public void smartOpen()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().intellect > 1)
        {
            Animator anim = door.GetComponent<Animator>();
            anim.SetTrigger("smart");
        }
    }

    public void closeDoor()
    {
        Animator anim = door.GetComponent<Animator>();
        anim.SetBool("isOpen", false);
    }
}
