using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulFactory : ItemFactory
{
    [SerializeField] GameObject soulPrefab;

    public override IItem getItem()
    {
        GameObject soulItem = Instantiate(soulPrefab);

        return soulItem.GetComponent<OrganItem>();
    }
}
