using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private int hp;
    [SerializeField] private int speed;
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
        float step = speed * Time.deltaTime;
        rb.transform.position = Vector3.MoveTowards(transform.position, nucleusTransform.position, step);
    }
}
