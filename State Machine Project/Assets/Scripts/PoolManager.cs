using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform enemyContainer;
    public int numberOfEnemies;

    private List<GameObject> enemyList;

    public void Init()
    {
        enemyList = new List<GameObject>();

        for(int i = 0; i < numberOfEnemies; i++)
        {
            GameObject obj = Instantiate(enemyPrefab, enemyContainer);
            obj.SetActive(false);
            enemyList.Add(obj);
        }
    }

    public GameObject GetEnemy()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            if(!enemyList[i].activeInHierarchy)
            {
                enemyList[i].SetActive(true);
                return enemyList[i];
            }
        }

        GameObject newEnemy = Instantiate(enemyPrefab, enemyContainer);
        newEnemy.SetActive(true);
        enemyList.Add(newEnemy);

        return newEnemy;
    }

    public void DisableEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
    }

    public void CleanPool()
    {
        foreach(GameObject enemy in enemyList)
        {
            enemy.SetActive(false);
        }
    }
}
