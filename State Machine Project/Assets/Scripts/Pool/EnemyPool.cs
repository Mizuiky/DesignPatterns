using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform enemyContainer;
    public int numberOfEnemies;

    private List<Enemy> enemyList;

    public void Init()
    {
        enemyList = new List<Enemy>();

        for(int i = 0; i < numberOfEnemies; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, enemyContainer);

            var newEnemy = enemy.GetComponent<Enemy>();

            if (newEnemy != null)
            {
                newEnemy.Init();

                newEnemy.Deactivate();

                enemyList.Add(newEnemy);
            }              
        }
    }

    public Enemy GetEnemy()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            if(!enemyList[i].gameObject.activeInHierarchy)
            {
                return enemyList[i];
            }
        }

        GameObject newEnemy = Instantiate(enemyPrefab, enemyContainer);

        var enemy = newEnemy.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.Init();
            enemyList.Add(enemy);

            return enemy;
        }

        return null;
    }

    public void CleanPool()
    {
        foreach(Enemy enemy in enemyList)
        {
            var newEnemy = enemy.GetComponent<Enemy>();

            if (newEnemy != null)
                newEnemy.Deactivate();
        }
    }
}
