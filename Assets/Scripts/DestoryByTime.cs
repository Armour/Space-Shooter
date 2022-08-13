using UnityEngine;
using System.Collections;

public class DestoryByTime : MonoBehaviour
{

    public float lifetime;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
