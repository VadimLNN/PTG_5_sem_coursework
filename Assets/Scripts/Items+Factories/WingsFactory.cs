using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingsFactory : ItemFactory
{
    [SerializeField] GameObject wingsPrefab;

    public override IItem getItem()
    {
        GameObject wingsItem = Instantiate(wingsPrefab);

        return wingsItem.GetComponent<OrganItem>();
    }
}
