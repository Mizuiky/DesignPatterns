using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolType
{
    Enemy
}

public class PoolCreator : MonoBehaviour
{
    [Header("Enemy pool setup")]

    public GameObject enemyPrefab;
    public Transform enemyContainer;

    public int enemyPoolSize;
    public Pool enemyPool;

    private List<IActivate> _enemypoolList;
    private IActivate aux;
    private Pool poolAux;

    public void Start()
    {
        Init();
    }

    public void Init()
    {
        _enemypoolList = new List<IActivate>();

        enemyPool = new Pool(_enemypoolList, enemyPrefab, enemyContainer, enemyPoolSize, PoolType.Enemy);

        CreatePool(enemyPool);
    }

    public void Reset()
    {
        foreach(IActivate poolComponent in enemyPool.poolList)
        {
            poolComponent.OnDeactivate();
        }
    }

    private void CreatePool(Pool pool)
    {

        for (int i = 0; i < pool.poolSize; i++)
        {
            
            var poolObject = CreatePoolObject(pool.prefab, pool.container);

            if (poolObject != null)
            {

                pool.poolList.Add(poolObject);
            }
        }       
    }

    private IActivate CreatePoolObject(GameObject prefab, Transform container)
    {
        GameObject obj = Instantiate(prefab, container);

        var newPoolObject = obj.GetComponent<IActivate>();

        if (newPoolObject != null)
        {
            newPoolObject.Init();

            return newPoolObject;
        }

        return null;
    }

    public IActivate GetPoolObject(PoolType type)
    {
        aux = null;
        poolAux = null;

        switch (type)
        {
            case PoolType.Enemy:

                poolAux = enemyPool;
                break;
        }

        for (int i = 0; i < poolAux.poolSize; i++)
        {
            if (!poolAux.poolList[i].IsActive)
            {
                return poolAux.poolList[i];
            }
        }

        aux = CreatePoolObject(poolAux.prefab, poolAux.container);

        if (aux != null)
        {
            poolAux.poolList.Add(aux);
            return aux;
        }
        
        return null;
    }
}

public class Pool
{
    public List<IActivate> poolList;
    public GameObject prefab;
    public Transform container;
    public int poolSize;
    public PoolType type;

    public Pool(List<IActivate> poolList, GameObject prefab, Transform container, int poolSize, PoolType type)
    {
        this.poolList = new List<IActivate>();

        this.poolList = poolList;
        this.prefab = prefab;
        this.container = container;
        this.poolSize = poolSize;
        this.type = type;
    }
}
