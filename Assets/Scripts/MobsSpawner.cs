using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobsSpawner : MonoBehaviour
{
    // ������ �� ������� ����� � ���� ���������
    public GameObject[] mobs;
    public GameObject mobsCrowd;

    // ���� ������
    public Vector3 spawnZone;
    
    // ����� ������, ������������ � �����������  
    float spawnWait;
    public float spawnMostWait;
    public float spawnLeastWait;
    
    // ����� �� ������ ������ ��������
    int startWait = 0;
    
    // ��������� ������ 
    public bool stop;

    // ����������� ����� 
    int flockCount;


    void Start()
    {
        StartCoroutine(waitSpawner());
    }

    void Update()
    {
        flockCount = mobsCrowd.transform.childCount;
        spawnWait = Random.Range(spawnLeastWait, spawnMostWait);

        if (flockCount > 15)
            stop = true;
        else 
            stop = false;
    }

    IEnumerator waitSpawner()
    {
        yield return new WaitForSeconds(startWait);

        while (!stop)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-spawnZone.x, spawnZone.x), 1, Random.Range(-spawnZone.z, spawnZone.z));

            Instantiate(mobs[Random.Range(0, mobs.Length-1)], spawnPos + transform.TransformPoint(0, 0, 0), gameObject.transform.rotation, mobsCrowd.transform);

            yield return new WaitForSeconds(spawnWait);
        }
    }

    private void OnDrawGizmos()
    {
        // ��������� ���� ������
        Vector3 spawnZoneCenter = mobsCrowd.transform.position;

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
