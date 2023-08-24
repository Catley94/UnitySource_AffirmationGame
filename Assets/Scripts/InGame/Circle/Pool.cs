using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Pool : MonoBehaviour
{
    
    private IObjectPool<GameObject> objectPool;

    public IObjectPool<GameObject> GetObjectPool()
    {
        return objectPool;
    }
    
    public void SetObjectPool(IObjectPool<GameObject> pool)
    {
        objectPool = pool;
    }
}
