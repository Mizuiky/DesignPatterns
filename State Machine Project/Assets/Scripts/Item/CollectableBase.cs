using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBase : MonoBehaviour
{
    public string stringToCompare;
    public float timeToDestroy;
    public int itemPoints;
    public SphereCollider collider;
    public GameObject graphicObject;

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

        if (collider != null)
            collider.enabled = false;

        if (graphicObject != null)
            graphicObject.SetActive(false);

        OnCollect();
    }

    public virtual void OnCollect()
    {
        //give points to player
        GameManager.Instance.ScoreManager.UpdateScore(itemPoints);

        gameObject.SetActive(false);

        //destroy game object
        Invoke(nameof(DestroyItem), timeToDestroy);
    }

    private void DestroyItem()
    {       
        Destroy(gameObject);
    }
}
