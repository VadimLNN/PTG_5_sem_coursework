using UnityEngine;
using UnityEngine.UI;

public class SpawnerMinonScr : InteractableObj
{
    // ������ �� ����� � ������ 
    public GameObject Minions;
    public GameObject minion;

    // ����������� ������ ������������ 
    float spawnRate = 10f;
    float nextSpawn = 0f;

    public Inventory inv;

    public override void interact()
    {
        SpawnMinion();
    }

    void SpawnMinion()
    {
        if (Time.time >= nextSpawn && inv.canGetItem(ItemTypes.Soul))
        {
            nextSpawn = Time.time + 1 / spawnRate;
            Instantiate(minion, transform.position, Quaternion.identity, Minions.gameObject.transform);
            inv.getItem(ItemTypes.Soul);
        }
    }
}
