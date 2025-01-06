using UnityEngine;

public class DialogScr : InteractableObj
{
    public GameObject panel;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        panel.SetActive(false);
    }

    public override void interact()
    {
        anim.SetInteger("state", 1);
        panel.SetActive(true); 
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void closePanel()
    {
        panel.SetActive(false);
        anim.SetInteger("state", 0);
        Cursor.lockState = CursorLockMode.Locked;
    }


}
