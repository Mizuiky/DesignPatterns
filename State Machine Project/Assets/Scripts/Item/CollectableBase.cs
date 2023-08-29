using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBase : MonoBehaviour
{
    public string stringToCompare;

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(stringToCompare))
        {
            Collect();
        }
    }

    public virtual void Collect()
    {
        //play audio
    }

    public virtual void OnCollect()
    {
        //give points to player

        //destroy game object
    }
}
