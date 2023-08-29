using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public Enemy enemy;
    public GameObject drop;

    public void OndropItem(Transform enemyPosition)
    {
        var item = Instantiate(drop, this.transform);
        item.transform.parent = null;
        item.transform.position = new Vector3(enemyPosition.position.x, 0.3f, enemyPosition.position.z);     
    }
}
