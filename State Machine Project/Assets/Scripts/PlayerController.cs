using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Movement")]

    public Rigidbody rb;
    public float speed;

    private Vector3 _movement;
    private float _horizontal;
    private float _vertical;

    [Header("Rotation")]

    public float rotationSpeed;
    private Quaternion newRotation;
    

    void Start()
    {
        
    }
 
    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
    }

    public void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _movement = new Vector3(_horizontal, 0, _vertical);
        _movement.Normalize();

        if(_movement != Vector3.zero)
        {
            rb.velocity = _movement * speed * Time.deltaTime;

            Rotate();
        }        
    }

    private void Rotate()
    {
        /**** creates a quaternion that will rotate in the movement direction using y axis ****/
        newRotation = Quaternion.LookRotation(_movement , Vector3.up);

        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);

    }
}
