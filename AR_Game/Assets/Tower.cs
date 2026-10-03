using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private int shootRadius;
    [SerializeField] private int fireRate;
    [SerializeField] private int hp;

    [SerializeField] private int cost;

    [SerializeField] private float fireCooldown;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform target;
    [SerializeField] private Transform firingSource;

    void Start()
    {
        Game_Manager.Instance.money -= 10;
    }


    void Update()
    {
        fireCooldown -= Time.deltaTime;

        GameObject target = FindClosestEnemy();

        if (target != null)
        {
            Vector3 direction = target.transform.position - transform.position;
            direction.y = 0f; 

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation,
                    targetRotation,
                    360f * Time.deltaTime
                );
            }

            if (fireCooldown <= 0f) 
            {
                Shoot(target);
                fireCooldown = 1f / fireRate;
            }

        }



    }

    GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject closest = null;
        float closestDistance = shootRadius;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = enemy;
            }
        }

        return closest;
    }

    void Shoot(GameObject target) 
    {
        GameObject projectile = Instantiate(
               projectilePrefab,
               firingSource.position,
               Quaternion.identity
           );

        Projectile projectileScript = projectile.GetComponent<Projectile>();

        if (projectileScript != null)
        {
            projectileScript.SetTarget(target.transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy") 
        {
            hp -= other.gameObject.GetComponent<Enemy>().damage;
        }
    }
}
