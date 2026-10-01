using UnityEngine;

public class Nucleus : MonoBehaviour
{

    [SerializeField] private int hp = 100;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int innerEnemySpawnRadius;
    [SerializeField] private int outerEnemySpawnRadius;
    [SerializeField] private float spawnTime = 100;
    [SerializeField] private float spawnTimer;
    void Start()
    {
        spawnTimer = spawnTime;
    }


    void Update()
    {
        spawnTimer -= 1 * Time.deltaTime;
        if (spawnTimer <= 0) 
        {
            Instantiate(enemyPrefab, new Vector3(
                Random.Range(innerEnemySpawnRadius, outerEnemySpawnRadius), 
                0,
                Random.Range(innerEnemySpawnRadius, outerEnemySpawnRadius)),
                new Quaternion(0, 0, 0, 0));

            spawnTimer = spawnTime;

        }
    }
}
