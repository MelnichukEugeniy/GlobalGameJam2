using System.Collections.Generic;
using UnityEngine;

public abstract class MalfunctionController<T> : MonoBehaviour where T : MalfunctionBase
{
    [SerializeField]
    private List<T> Malfunctions = new List<T>();

    protected void Awake()
    {
        
    }

    protected virtual void Update()
    {
        Malfunctions.ForEach(x => x.Update());
    }
}