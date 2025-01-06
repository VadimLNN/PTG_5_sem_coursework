using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestHolder : MonoBehaviour
{
    public int points = 1;
    public QuestManager questManager;

    private void OnDestroy()
    {
        foreach (var quest in questManager.quests)
        {
            if (quest.questName.Contains(transform.name))
            {
                questManager.UpdateQuest(quest, points);
            }
        }
    }
}
