using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;

    private Transform target;

    float autoDestruction = 1f;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;

        transform.position += direction.normalized * speed * Time.deltaTime;

        autoDestruction -= 1 * Time.deltaTime;
        if (autoDestruction <= 0) { Destroy(gameObject); }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Tower" && other.gameObject.tag != "Enemy")
        {
            Destroy(gameObject);
        }
    }

}
