using UnityEngine;

public interface IPoolable
{
    public GameObject ObjectInstance { get; }
    
    public void OnSpawn();
    public void OnRelease();
}