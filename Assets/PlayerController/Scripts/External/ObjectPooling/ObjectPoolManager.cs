using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

[Serializable]
public struct ObjectPoolParams
{
    [Tooltip("A unique identifier for the object pool. Used to distinguish between different pools.")]
    public string Tag;

    [Tooltip("If true, the pool checks to ensure objects being released are part of the pool. Enable this for debugging or safety.")]
    public bool CollectionCheck;

    [Tooltip("The initial number of objects to allocate in the pool. Helps reduce runtime allocations.")]
    public int DefaultCapacity;

    [Tooltip("The maximum number of objects the pool can hold. Objects exceeding this limit will be destroyed when returned.")]
    public int MaxSize;
}

public class ObjectPoolManager : IObjectPool
{
    protected readonly Dictionary<(string, Type), object> ObjectPoolsMap = new();

    public ObjectPoolManager()
    {
        
    }
    
    public bool CreatePool<T>(Func<T> factoryMethod, ObjectPoolParams poolParams, out IObjectPool<T> pool) where T : class, IPoolable
    {
        var type = typeof(T);
        var key = (poolParams.Tag, type);
        
        pool = null;
        if (ObjectPoolsMap.ContainsKey(key))
            return false;
        
        var objectPool = new ObjectPool<T>(
            factoryMethod, 
            Get, 
            Release, 
            OnDestroy, 
            poolParams.CollectionCheck, 
            poolParams.DefaultCapacity, 
            poolParams.MaxSize);

        ObjectPoolsMap[key] = objectPool;

        pool = objectPool;
        return true;
    }

    public bool DestroyPool<T>(T prefab, string tag) where T : class, IPoolable
    {
        var type = typeof(T);
        var key = (tag, type);
        
        if(!ObjectPoolsMap.TryGetValue(key, out var pool))
            return false;

        var typePool = (ObjectPool<T>) pool;

        typePool?.Dispose();

        ObjectPoolsMap.Remove(key);

        return true;
    }
    
    protected virtual void Get(IPoolable poolable)
    {
        poolable.OnSpawn();
    }
    
    protected virtual void Release(IPoolable poolable)
    {
        poolable.OnRelease();
    }

    protected virtual void OnDestroy(IPoolable poolable)
    {
        Object.Destroy(poolable.ObjectInstance);
    }
}