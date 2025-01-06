using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] List<ItemProbability> itemFactoriesWithProbs = new List<ItemProbability>();
    List<ItemFactory> itemFactories = new List<ItemFactory>();

    ItemFactory itemFactory;

    private void Start()
    {
        float probabilitySum = 0;

        foreach (var item in itemFactoriesWithProbs)
            probabilitySum += item.probability;

        foreach (var item in itemFactoriesWithProbs)
            item.probability = Mathf.Floor((item.probability / probabilitySum) * 100);

        foreach (var item in itemFactoriesWithProbs)
            for (int i = 0; i < item.probability; i++)
                itemFactories.Add(item.factory);
    }

    public void spawnRandomItem()
    {
        itemFactory = itemFactories[Random.Range(0, itemFactories.Count)];

        IItem item = itemFactory.getItem();

        Vector3 direction = new Vector3 (Random.insideUnitCircle.x , 0, Random.insideUnitCircle.y);
        direction = direction.normalized * Random.Range(3, 6);
        Vector3 position = transform.position + direction;

        item.setPosition(position);
    }

    public void spawnRandomItem(Vector3 position)
    {
        itemFactory = itemFactories[Random.Range(0, itemFactories.Count)];

        IItem item = itemFactory.getItem();

        item.setPosition(position);
    }

    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Tab)) 
        //    spawnRandomItem();
    }

}
