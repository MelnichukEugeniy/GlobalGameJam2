using System.Collections.Generic;
using UnityEngine;

public abstract class MalfunctionController<T> : MonoBehaviour where T : MalfunctionBase
{
    [field: SerializeField]
    public List<T> Malfunctions { get; private set; } = new List<T>();
}