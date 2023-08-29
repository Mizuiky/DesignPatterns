using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAnimation : MonoBehaviour
{
    public float speed;
    public float degreesPerSecond;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, degreesPerSecond, 0) * speed * Time.deltaTime);
    }
}
