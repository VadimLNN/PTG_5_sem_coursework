using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalFactory : ItemFactory
{
    [SerializeField] GameObject cristalPrefab;

    public override IItem getItem()
    {
        GameObject cristalItem = Instantiate(cristalPrefab);

        return cristalItem.GetComponent<OrganItem>();
    }
}
