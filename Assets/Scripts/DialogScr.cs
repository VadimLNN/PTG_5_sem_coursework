using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class DialogScr : InteractableObj
{
    public GameObject panel;
    Animator anim;

    public Text dlgTxt;

    public QuestManager qm;

    void Start()
    {
        anim = GetComponent<Animator>();
        panel.SetActive(false);
    }

    public override void interact()
    {
        anim.SetInteger("state", 1);
        panel.SetActive(true); 
        Cursor.lockState = CursorLockMode.None;

        int finalQuests = 0;
        foreach (Quest q in qm.quests)
            if (q.currentAmount == q.targetAmount)
                finalQuests++;

        if (finalQuests == qm.quests.Count)
            dlgTxt.text = "Молодец. Сварю из этого что-нить балдёжное, зайди позже.";
        else if (finalQuests != qm.quests.Count && qm.questListUI.childCount != 0)
            dlgTxt.text = "Ещё не всё собрал. Собирай дальше.";

        closePanel();
    }
    public void closePanel()
    {
        panel.SetActive(false);
        anim.SetInteger("state", 0);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void quests()
    {
        qm.showQuests();
        closePanel();
    }
}
