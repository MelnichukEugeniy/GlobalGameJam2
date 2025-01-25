using System;
using UnityEngine.Pool;

public interface IObjectPool
{
    public bool CreatePool<T>(Func<T> factoryMethod, ObjectPoolParams poolParams, out IObjectPool<T> pool) where T : class, IPoolable;

    public bool DestroyPool<T>(T prefab, string tag) where T : class, IPoolable;
}