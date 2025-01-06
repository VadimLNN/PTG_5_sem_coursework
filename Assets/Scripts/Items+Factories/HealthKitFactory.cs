using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKitFactory : ItemFactory
{
    [SerializeField] GameObject healthKitPrefab;

    public override IItem getItem()
    {
        GameObject healthKit = Instantiate(healthKitPrefab);

        return healthKit.GetComponent<HealthKit>();
    }
}
