using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public delegate void action();

public class DialogueSystem : MonoBehaviour
{
    public GameObject dialogueWindow;
    public GameObject answers;
    public TextMeshProUGUI message;
    public TextMeshProUGUI answer;

    Dictionary<string, action> actions = new Dictionary<string, action>();

    CDialogue dialogue = new CDialogue();

    public void loadDialogue(TextAsset xmlAsset)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlAsset.text);

        dialogue.Clear();    // очистка диалога
        actions.Clear();     // очистка и инициализация списка действий

        actions.Add("none", null);
        actions.Add("door open", null);
        actions.Add("dialogue end", dialogueEnd);
        actions.Add("dialogue change", null);
        actions.Add("make smarter", null);
        actions.Add("make stronger", null);

        XmlNode messages = xmlDoc.SelectSingleNode("/messages");
        XmlNodeList messageNodes = xmlDoc.SelectNodes("/messages/message");

        foreach (XmlNode messageNode in messageNodes)
        {
            CMessage msg = new CMessage();
            msg.msgID = long.Parse(messageNode.Attributes["uid"].Value);

            // Получаем текст из узла <text>
            XmlNode textNode = messageNode["text"];
            msg.text = textNode != null ? textNode.InnerText : "";

            dialogue.loadMessage(msg);

            XmlNode answersNode = messageNode["answers"];
            if (answersNode != null)
            {
                foreach (XmlNode answerNode in answersNode.SelectNodes("answer"))
                {
                    CAnswer answ = new CAnswer();

                    answ.answID = long.Parse(answerNode.Attributes["auid"].Value);
                    answ.msgID = long.Parse(answerNode.Attributes["uid"].Value);
                    answ.action = answerNode.Attributes["action"]?.Value ?? "none";
                    answ.text = answerNode.InnerText;

                    dialogue.loadAnswer(answ);
                }
            }
        }

        // Показываем первое сообщение, если оно есть
        if (dialogue.getMessages().Count > 0)
        {
            showMessage(dialogue.getMessages()[0].msgID, "none");
        }

        dialogueWindow.SetActive(true);
    }

    public void showMessage(long uid, string act)
    {
        actions[act]?.Invoke();
        if (uid == -1) return;
        foreach(Transform child in answers.transform)
            Destroy(child.gameObject);

        message.text = dialogue.selectMessage(uid);

        foreach (CAnswer ans in dialogue.getAnswers())
        {
            TextMeshProUGUI txt = Instantiate<TextMeshProUGUI>(answer);
            txt.text = ans.text;

            txt.GetComponent<Button>().onClick.AddListener(delegate { showMessage(ans.msgID, ans.action);  });
            txt.transform.SetParent(answers.transform);
        }
    }

    public void dialogueEnd()
    {
        dialogueWindow.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void setAction(string name, action act)
    {
        actions[name] = act;
    }

}
