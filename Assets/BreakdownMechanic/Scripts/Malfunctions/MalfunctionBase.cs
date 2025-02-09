using System;
using UnityEngine;

public abstract class MalfunctionBase : ScriptableObject, IDisposable
{
    public event Action<MalfunctionBase> MalfunctionDetected; 

    public abstract void Initialize();
    
    public abstract void Update();

    public abstract bool IsMalfunctionDetected();
    public abstract void Fix();
    
    public abstract void Show();
    public abstract void Hide();
    
    public abstract void Solve();
    
    public abstract void Dispose();
    
    public void InvokeMalfunctionDetected()
    {
        MalfunctionDetected?.Invoke(this);
    }
}