using System;
using UnityEngine;

[Serializable]
public class Observable<T> : ISerializationCallbackReceiver
{
    public event Action<T> OnChanged;

    [SerializeField] private T value; // Used by Unity Inspector

    public T Value
    {
        get => value;
        set
        {
            if (!Equals(this.value, value))
            {
                this.value = value;
                OnChanged?.Invoke(value);
            }
        }
    }

    public Observable() { value = default; }

    public Observable(T value) { this.value = value; }

    public void OnBeforeSerialize() { }

    public void OnAfterDeserialize()
    {
        OnChanged?.Invoke(value);
    }

    public override string ToString() => value?.ToString() ?? "null";
}