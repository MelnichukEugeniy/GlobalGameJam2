using UnityEngine;

public abstract class FixAction : ScriptableObject
{
    [field: SerializeField]
    public string Text { get; private set; }
    public abstract void PerformAction();
}