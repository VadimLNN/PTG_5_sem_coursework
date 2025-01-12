using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomFactory : ItemFactory
{
    [SerializeField] GameObject mushroomPrefab;

    public override IItem getItem()
    {
        GameObject mushroomItem = Instantiate(mushroomPrefab);

        return mushroomItem.GetComponent<OrganItem>();
    }
}
