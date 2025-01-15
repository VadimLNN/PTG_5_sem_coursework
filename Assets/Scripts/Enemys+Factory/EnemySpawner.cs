using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] 
    List<EnemyProbability> enemyFactoriesWithProbs = new List<EnemyProbability>();

    public List<EnemyFactory> enemyFactories = new List<EnemyFactory>();

    EnemyFactory enemyFactory;
    public Transform player;

    public float spawnIntervalMax = 10f;
    public float spawnIntervalMin = 1.5f;

    public bool spawningEveryWhere = false;

    private void Start() 
    {
        float probabilitySum = 0;

        foreach (var enemy in enemyFactoriesWithProbs)
            probabilitySum += enemy.probability;

        foreach (var enemy in enemyFactoriesWithProbs)
            enemy.probability = Mathf.Floor((enemy.probability / probabilitySum) * 100);

        foreach (var enemy in enemyFactoriesWithProbs)
            for (int i = 0; i < enemy.probability; i++)
                enemyFactories.Add(enemy.factory);

        InvokeRepeating(nameof(SpawnEnemy), spawnIntervalMin, Random.Range(spawnIntervalMin, spawnIntervalMax));
    }
    void Update()
    {
        
    }

    public void spawnRandomEnemy()
    {
        enemyFactory = enemyFactories[Random.Range(0, enemyFactories.Count)];
        IEnemy enemy = enemyFactory.getEnemy();


        Vector3 direction = new Vector3(Random.insideUnitCircle.x, 0, Random.insideUnitCircle.y);
        direction = direction.normalized * Random.Range(3, 50);
        Vector3 position = transform.position + direction;
        
        enemy.positionAndRotation(position, Quaternion.identity);
        
        enemy.Target = player;

        Health enemyHP = enemy.EnemyHP;
        enemyHP.spawnOnDeath.AddListener(transform.GetComponent<ItemSpawner>().spawnItem);

    }

    void SpawnEnemy()
    {
        if (spawningEveryWhere)
        {
            spawnRandomEnemy();

            spawnIntervalMax = Mathf.Max(1f, spawnIntervalMax - 0.05f);
            CancelInvoke(nameof(SpawnEnemy));
            InvokeRepeating(nameof(SpawnEnemy), spawnIntervalMin, spawnIntervalMax);
        }
    }

    public List<EnemyFactory> getFactories()
    {
        return enemyFactories;
    }

    public void spawnRandomEnemy(Transform enemysCrowd, int distX, int sidtZ)
    {
        enemyFactory = enemyFactories[Random.Range(0, enemyFactories.Count)];
        IEnemy enemy = enemyFactory.getEnemy(enemysCrowd);


        Vector3 spawnZoneCenter = enemysCrowd.transform.position;

        Vector3 spawnPos = new Vector3(Random.Range(spawnZoneCenter.x - distX, spawnZoneCenter.x + distX),
                                        spawnZoneCenter.y,
                                        Random.Range(spawnZoneCenter.z - sidtZ, spawnZoneCenter.z + sidtZ));

        enemy.positionAndRotation(spawnPos, Quaternion.identity);
        
        enemy.Target = player;

        Health enemyHP = enemy.EnemyHP;
        enemyHP.spawnOnDeath.AddListener(transform.GetComponent<ItemSpawner>().spawnItem);
    }
}


