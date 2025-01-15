using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnInZone : MonoBehaviour
{
    public Transform enemysCrowd;

    // зона спавна
    public int x;
    public int z;

    // состояние спавна 
    public bool stop;

    // численность стада 
    public int maxCrowdCount = 10;

    public EnemySpawner spawnerMain;

    void Update()
    {
        if (enemysCrowd.transform.childCount <= maxCrowdCount)
            stop = false;
        else
            stop = true;

        if (stop == true) return;

        spawnerMain.spawnRandomEnemy(enemysCrowd, x, z);
    }

    //IEnumerator waitSpawner()
    //{
    //    yield return new WaitForSeconds(10);

    //    while (!stop)
    //    {
    //        Vector3 spawnZoneCenter = enemysCrowd.transform.position;

    //        Vector3 spawnPos = new Vector3( Random.Range(spawnZoneCenter.x - spawnZone.x, spawnZoneCenter.x + spawnZone.x),
    //                                        spawnZoneCenter.y, 
    //                                        Random.Range(spawnZoneCenter.z - spawnZone.z, spawnZoneCenter.z + spawnZone.z));

    //        IEnemy enemy = enemyFactories[Random.Range(0, enemyFactories.Length)].getEnemy(crowd);

    //        enemy.positionAndRotation(spawnPos, Quaternion.identity);

    //        enemy.Target = player;

    //        Health enemyHP = enemy.EnemyHP;
    //        enemyHP.spawnOnDeath.AddListener(spawnerMain.transform.GetComponent<ItemSpawner>().spawnItem);

    //        yield return new WaitForSeconds(spawnWait);
    //    }
    //}

    private void OnDrawGizmos()
    {
        // отрисовка зоны спавна
        Vector3 spawnZoneCenter = enemysCrowd.transform.position;

        Vector3 zoneLU = new Vector3(spawnZoneCenter.x - x, spawnZoneCenter.y, spawnZoneCenter.z + z);
        Vector3 zoneRU = new Vector3(spawnZoneCenter.x + x, spawnZoneCenter.y, spawnZoneCenter.z + z);
        Vector3 zoneRD = new Vector3(spawnZoneCenter.x + x, spawnZoneCenter.y, spawnZoneCenter.z - z);
        Vector3 zoneLD = new Vector3(spawnZoneCenter.x - x, spawnZoneCenter.y, spawnZoneCenter.z - z);

        Gizmos.DrawLine(zoneLU, zoneRU);
        Gizmos.DrawLine(zoneRU, zoneRD);
        Gizmos.DrawLine(zoneRD, zoneLD);
        Gizmos.DrawLine(zoneLD, zoneLU);
    }
}
