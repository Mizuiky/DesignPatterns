using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform player;

    public float distance;

    public float high;

    public float speed;

    void LateUpdate()
    {
        //Done after Update, player has finished it movement;

        Vector3 camPosition = player.transform.position - (player.transform.forward * distance) - transform.up * high;

        transform.position = camPosition;

        transform.rotation = Quaternion.Slerp(transform.rotation, player.rotation, speed);
        
    }
}
