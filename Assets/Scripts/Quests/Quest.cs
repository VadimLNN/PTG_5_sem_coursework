[System.Serializable]
public class Quest
{
    public string questName;
    public string description;
    public int targetAmount;
    public int currentAmount;

    public Quest (string questName, string description, int targetAmount, int currentAmount)
    {
        this.questName = questName;
        this.description = description;
        this.targetAmount = targetAmount;
        this.currentAmount = currentAmount;
    }

    public bool IsComplete => currentAmount >= targetAmount;

    public void AddProgress(int amount)
    {
        currentAmount += amount;
        if (currentAmount > targetAmount) currentAmount = targetAmount;
    }

    public void ResetProgress()
    {
        currentAmount = 0;
    }
}
