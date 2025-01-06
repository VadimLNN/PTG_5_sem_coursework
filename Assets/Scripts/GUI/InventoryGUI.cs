using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class ItemGUI
{
    public ItemTypes itemType;
    public TMP_Text text;
}

public class InventoryGUI : MonoBehaviour
{
    public Inventory inventory;

    public List<ItemGUI> ItemList;
    Dictionary<ItemTypes, TMP_Text> itemDictionary;

    public void listToDictionary()
    {
        itemDictionary = new Dictionary<ItemTypes, TMP_Text>();

        foreach (var item in ItemList)
            if (itemDictionary.ContainsKey(item.itemType) == false)
                itemDictionary.Add(item.itemType, item.text);
    }

    void Start() 
    {
        listToDictionary(); 
        updateGUI();
    }

    public void updateGUI()
    {
        if (itemDictionary != null) 
            foreach(KeyValuePair<ItemTypes, int> kvp in inventory.itemDictionary)
                if(itemDictionary.ContainsKey(kvp.Key))
                    itemDictionary[kvp.Key].text = kvp.Value.ToString();
    }
}
