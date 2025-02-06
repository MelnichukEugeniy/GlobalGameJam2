using System;
using UnityEngine;

public abstract class MalfunctionBase : ScriptableObject, IDisposable
{
    public abstract void Initialize();
    
    public abstract void Update();

    public abstract bool IsMalfunctionDetected();
    public abstract void Dispose();
    
}