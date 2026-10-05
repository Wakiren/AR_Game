using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private int hp;
    [SerializeField] private int speed;
    [SerializeField] public int damage;
    [SerializeField] private int value;
    [SerializeField] private Transform nucleusTransform;
    [SerializeField] private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        nucleusTransform = GameObject.FindGameObjectWithTag("Nucleus").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed *0.1f* Time.deltaTime;
        rb.transform.position = Vector3.MoveTowards(transform.position, nucleusTransform.position, step);

        if (hp <= 0) 
        {
            Game_Manager.Instance.money += value;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            hp -= other.gameObject.GetComponent<Projectile>().damage;
            Destroy(other.gameObject);
        }
    }

}
