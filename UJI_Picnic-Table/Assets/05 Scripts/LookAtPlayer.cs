using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
    }
    void Update()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = lookRotation * Quaternion.Euler(0f, 180f, 0f);
    }
}