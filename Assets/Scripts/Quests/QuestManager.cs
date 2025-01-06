using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public List<Quest> quests = new List<Quest>(); 
    public Transform questListUI; 
    public GameObject questUIPrefab;

    private Dictionary<Quest, GameObject> questUIElements = new Dictionary<Quest, GameObject>();

    private void Start()
    {
        ResetProgress();
        //LoadProgress();
        UpdateQuestUI();
    }

    public void UpdateQuest(Quest quest, int amount)
    {
        if (quest != null && !quest.IsComplete)
        {
            quest.AddProgress(amount);
            UpdateQuestUI();
            //SaveProgress();
        }
    }

    public void UpdateQuest(string item, int points)
    {
        foreach (var quest in quests)
        {
            if (quest.questName.Contains(item))
            {
                UpdateQuest(quest, points);
            }
        }
    }

    private void UpdateQuestUI()
    {
        foreach (var quest in quests)
        {
            if (!questUIElements.ContainsKey(quest))
            {
                // Создаем новый UI элемент для задания
                GameObject questUI = Instantiate(questUIPrefab, questListUI);
                questUIElements[quest] = questUI;
            }

            var texts = questUIElements[quest].GetComponentsInChildren<TMP_Text>();
            texts[0].text = quest.questName; 
            texts[1].text = $"{quest.currentAmount}/{quest.targetAmount}";
        }
    }

    public void SaveProgress()
    {
        foreach (var quest in quests)
        {
            PlayerPrefs.SetInt(quest.questName + "_progress", quest.currentAmount);
        }
    }

    public void LoadProgress()
    {
        foreach (var quest in quests)
        {
            if (PlayerPrefs.HasKey(quest.questName + "_progress"))
            {
                quest.currentAmount = PlayerPrefs.GetInt(quest.questName + "_progress");
            }
        }
    }

    public void ResetProgress()
    {
        foreach (var quest in quests)
        {
            PlayerPrefs.DeleteKey(quest.questName + "_progress");
        }
    }
}
