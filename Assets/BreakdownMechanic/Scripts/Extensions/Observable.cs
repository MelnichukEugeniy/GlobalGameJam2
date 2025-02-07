using System;
using UnityEngine;

[Serializable]
public class Observable<T>
{
    public event Action<T> OnChanged;

    [SerializeField] private T value; // Unity needs this to be serialized

    public T Value
    {
        get => value;
        set
        {
            this.value = value;
            OnChanged?.Invoke(value);
        }
    }

    public Observable() { value = default; }

    public Observable(T value) { this.value = value; }
}