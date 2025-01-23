using System;
using UnityEngine;

public abstract class BasePlayerFeature : ScriptableObject, IDisposable
{
    protected PlayerController playerController;
    
    public virtual void InitializeWithPlayer(PlayerController player)
    {
        playerController = player;
    }
    
    public virtual void Update() {}
    
    public virtual void Dispose() {}
}