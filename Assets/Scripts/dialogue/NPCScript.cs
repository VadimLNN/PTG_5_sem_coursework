using UnityEngine;

public class NPCScript : MonoBehaviour
{
    public TextAsset currentDialogue;
    public TextAsset nextDialog;
    public GameObject dialogueSystem;
    public Animator animator;

    public float interactionDistance = 3f;
    public KeyCode interactKey = KeyCode.F;

    private GameObject player;
    private NPCScript npc;

    bool isOpen = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        npc = GetComponent<NPCScript>();
    }

    void Update()
    {
        if (player == null || npc == null) return;

        float dist = Vector3.Distance(transform.position, player.transform.position);

        if (dist <= interactionDistance && Input.GetKeyDown(interactKey))
        {
            interact();
        }
    }

    public void interact()
    {
        if (animator != null)
        {
            animator.SetInteger("State", 1);
        }

        ShowCursor();

        dialogueSystem.SetActive(true);

        dialogueSystem.GetComponent<DialogueSystem>().loadDialogue(currentDialogue);
        dialogueSystem.GetComponent<DialogueSystem>().setAction("dialogue change", changeDlg);
    }

    public void changeDlg()
    {
        currentDialogue = nextDialog;
    }

    void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
