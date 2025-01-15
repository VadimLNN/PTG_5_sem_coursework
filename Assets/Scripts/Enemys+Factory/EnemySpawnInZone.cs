using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnInZone : MonoBehaviour
{
    // время спавна, максимальное и минимальное  
    float spawnWait;
    public float spawnMostWait;
    public float spawnLeastWait;

    public Transform enemysCrowd;

    // зона спавна
    public Vector3 spawnZone;

    // состояние спавна 
    public bool stop;

    // численность стада 
    public int maxCrowdCount = 10;
    public Transform crowd;

    public Transform player;
    EnemyFactory enemyFactory;

    public GameObject spawnerMain;

    public EnemyFactory[] enemyFactories;

    void Start()
    {
        StartCoroutine(waitSpawner());
    }

    void Update()
    {
        if (enemysCrowd.transform.childCount == maxCrowdCount)
            stop = true;
        else 
            stop = false;
    }

    IEnumerator waitSpawner()
    {
        yield return new WaitForSeconds(10);

        while (!stop)
        {
            Vector3 spawnZoneCenter = enemysCrowd.transform.position;

            Vector3 spawnPos = new Vector3( Random.Range(spawnZoneCenter.x - spawnZone.x, spawnZoneCenter.x + spawnZone.x),
                                            spawnZoneCenter.y, 
                                            Random.Range(spawnZoneCenter.z - spawnZone.z, spawnZoneCenter.z + spawnZone.z));

            IEnemy enemy = enemyFactories[Random.Range(0, enemyFactories.Length)].getEnemy(crowd);

            enemy.positionAndRotation(spawnPos, Quaternion.identity);

            enemy.Target = player;

            Health enemyHP = enemy.EnemyHP;
            enemyHP.spawnOnDeath.AddListener(spawnerMain.transform.GetComponent<ItemSpawner>().spawnItem);

            yield return new WaitForSeconds(spawnWait);
        }
    }

    private void OnDrawGizmos()
    {
        // отрисовка зоны спавна
        Vector3 spawnZoneCenter = enemysCrowd.transform.position;

        Vector3 zoneLU = new Vector3(spawnZoneCenter.x - spawnZone.x, spawnZoneCenter.y, spawnZoneCenter.z + spawnZone.z);
        Vector3 zoneRU = new Vector3(spawnZoneCenter.x + spawnZone.x, spawnZoneCenter.y, spawnZoneCenter.z + spawnZone.z);
        Vector3 zoneRD = new Vector3(spawnZoneCenter.x + spawnZone.x, spawnZoneCenter.y, spawnZoneCenter.z - spawnZone.z);
        Vector3 zoneLD = new Vector3(spawnZoneCenter.x - spawnZone.x, spawnZoneCenter.y, spawnZoneCenter.z - spawnZone.z);

        Gizmos.DrawLine(zoneLU, zoneRU);
        Gizmos.DrawLine(zoneRU, zoneRD);
        Gizmos.DrawLine(zoneRD, zoneLD);
        Gizmos.DrawLine(zoneLD, zoneLU);
    }
}
