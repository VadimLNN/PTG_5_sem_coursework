using UnityEngine;
using UnityEngine.UI;

public class DialogScr : InteractableObj
{
    public GameObject panel;
    Animator anim;

    public Text dlgTxt;

    public QuestManager qm;

    bool panelOpen = false;
    bool showQuests = true;

    void Start()
    {
        anim = GetComponent<Animator>();
        panel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && panelOpen)
        {
            if (showQuests)
            {
                quests();
                showQuests = false;
            }
            closePanel();
        }
    }
    public override void interact()
    {
        anim.SetInteger("state", 1);
        panel.SetActive(true);
        panelOpen = true;

        GameObject gameManager = GameObject.FindWithTag("GameManager");
        if (gameManager != null)
            gameManager.GetComponent<CursorLock>().unlockCursor();


        int finalQuests = 0;
        foreach (Quest q in qm.quests)
            if (q.currentAmount == q.targetAmount)
                finalQuests++;

        if (finalQuests == qm.quests.Count)
            dlgTxt.text = "Good for you. I'll make something bald, come back later.";
        else if (finalQuests != qm.quests.Count && qm.questListUI.childCount != 0)
            dlgTxt.text = "You haven't gotten it all together yet. Keep collecting.";
    }
    public void closePanel()
    {
        panel.SetActive(false);
        panelOpen = false;
        anim.SetInteger("state", 0);

        GameObject gameManager = GameObject.FindWithTag("GameManager");
        if (gameManager != null)
            gameManager.GetComponent<CursorLock>().lockCursor();
    }

    public void quests()
    {
        closePanel();
        qm.showQuests();
    }
}
