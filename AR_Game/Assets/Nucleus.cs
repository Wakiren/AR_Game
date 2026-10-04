using UnityEngine;

public class Nucleus : MonoBehaviour
{

    [SerializeField] private int hp = 100;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float innerEnemySpawnRadius;
    [SerializeField] private float outerEnemySpawnRadius;
    [SerializeField] private float spawnTime = 100;
    [SerializeField] private float spawnTimer;
    [SerializeField] float spawnRadiusCheck;

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



            if (!Physics.CheckSphere(spawnPosition, spawnRadiusCheck, spawnBlockingLayer))
            {
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

                spawnTimer = spawnTime;
            }
        }

        Debug.Log("Nucleus HP" + hp);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            hp -= other.gameObject.GetComponent<Enemy>().damage;
            Destroy(other.gameObject);
        }
    }
}
