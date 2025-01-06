using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsFactory : ItemFactory
{
    [SerializeField] List<GameObject> organItems = new List<GameObject>();

    public override IItem getItem()
    {
        GameObject organItem = Instantiate(organItems[Random.Range(0, organItems.Count)]);

        return organItem.GetComponent<OrganItem>();
    }
}
