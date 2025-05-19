using System.Collections.Generic;

public class CDialogue
{
    List<CMessage> messages = new List<CMessage> ();
    long UID = 0;
    CMessage selectedMessage = null;
    CAnswer selectedAnswer = null;

    long getUID()
    {
        UID++;
        return UID;
    }

    CMessage findMsg(long msgID)
    {
        foreach (var msg in messages)
            if (msg.msgID == msgID)
                return msg;

        return null;
    }

    CAnswer findAnsw(long answID)
    {
        foreach (var ans in selectedMessage.answers)
            if (ans.answID == answID)
                return ans;

        return null;
    }

    public string selectMessage(long msgID)
    {
        selectedMessage = findMsg(msgID);
        return selectedMessage.text;
    }

    public string selectAnswer(long msgID, long answID)
    {
        selectMessage(msgID);
        selectedAnswer = findAnsw(answID);

        return selectedAnswer.text + " [action : " + selectedAnswer.action + "]";
    }

    public List<CAnswer> getAnswers()
    {
        return selectedMessage.answers;
    }
    public List<CMessage> getMessages()
    {
        return messages;
    }
    
    public long linkedUID()
    {
        if (selectedAnswer != null)
            return selectedAnswer.msgID;

        return -1;
    }

    public void Clear()
    {
        messages.Clear();
        UID = 0;
        selectedMessage = null;
        selectedAnswer = null;
    }

    public void loadMessage(CMessage msg)
    {
        messages.Add(msg);
        selectedMessage = msg;
    }

    public void loadAnswer(CAnswer answ)
    {
        selectedMessage.answers.Add(answ);
    }
}
