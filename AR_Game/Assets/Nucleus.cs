using UnityEngine;

public class Nucleus : MonoBehaviour
{

    [SerializeField] private int hp = 100;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int innerEnemySpawnRadius;
    [SerializeField] private int outerEnemySpawnRadius;
    [SerializeField] private float spawnTime = 100;
    [SerializeField] private float spawnTimer;

    [SerializeField] private LayerMask spawnBlockingLayer;

    void Start()
    {
        spawnTimer = spawnTime;
    }


    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-outerEnemySpawnRadius, outerEnemySpawnRadius),0,
            Random.Range(-outerEnemySpawnRadius, outerEnemySpawnRadius));

            float spawnRadius = 1.5f;

            if (!Physics.CheckSphere(spawnPosition, spawnRadius, spawnBlockingLayer))
            {
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

                spawnTimer = spawnTime;
            }
        }

    }
}
