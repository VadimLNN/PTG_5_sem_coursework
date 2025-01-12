using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField]
    List<ItemProbability> itemFactoriesWithProbs = new List<ItemProbability>();
    
    public List<ItemFactory> itemFactories = new List<ItemFactory>();

    ItemFactory itemFactory;

    private void Start()
    {
        //float probabilitySum = 0;

        //foreach (var item in itemFactoriesWithProbs)
        //    probabilitySum += item.probability;

        //foreach (var item in itemFactoriesWithProbs)
        //    item.probability = Mathf.Floor((item.probability / probabilitySum) * 100);

        //foreach (var item in itemFactoriesWithProbs)
        //    for (int i = 0; i < item.probability; i++)
        //        itemFactories.Add(item.factory);
    }

    //public void spawnRandomItem()
    //{
    //    itemFactory = itemFactories[Random.Range(0, itemFactories.Count)];

    //    IItem item = itemFactory.getItem();

    //    Vector3 direction = new Vector3 (Random.insideUnitCircle.x , 0, Random.insideUnitCircle.y);
    //    direction = direction.normalized * Random.Range(3, 6);
    //    Vector3 position = transform.position + direction;

    //    item.setPosition(position);
    //}

    //public void spawnRandomItem(Vector3 position)
    //{
    //    itemFactory = itemFactories[Random.Range(0, itemFactories.Count)];

    //    IItem item = itemFactory.getItem();

    //    item.setPosition(position);
    //}

    public void spawnItem(Vector3 position, string type)
    {
        switch (type)
        {
            case "cristal":
                itemFactory = itemFactories[0];
                break;
            case "mushroom":
                itemFactory = itemFactories[1];
                break;
            case "wings":
                itemFactory = itemFactories[2];
                break;
            case "soul":
                itemFactory = itemFactories[3];
                break;
            default:
                itemFactory = itemFactories[3];
                break;
        }

        IItem item = itemFactory.getItem();

        item.setPosition(position);
    }
}
