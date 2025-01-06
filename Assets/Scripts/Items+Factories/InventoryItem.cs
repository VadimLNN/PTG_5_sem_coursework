using System;

public enum ItemTypes { Cristal, Wings, Soul, Mushroom, Cerebellum, Ear, Pinky};

[Serializable]

public struct InventoryItem
{
    public ItemTypes type;
    public int quant;
}
