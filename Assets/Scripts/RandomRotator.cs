using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour
{

    public float tumble;

    private Rigidbody rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = Random.insideUnitSphere * tumble;
    }
}
