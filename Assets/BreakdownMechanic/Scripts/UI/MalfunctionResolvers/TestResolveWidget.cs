using System;
using UnityEngine;

public class TestResolveWidget : MalfunctionResolveBase<VentMalfunction>
{
    [field: SerializeField]
    public string InstructionText;
    
    public event Action<VentMalfunctionResolveWidget, bool> MalfunctionResolved;
    protected override void OnMalfunctionResolvedCallback()
    {
        base.OnMalfunctionResolvedCallback();
        Debug.Log("Malfunction resolved");
    }

    protected override void OnMalfunctionUnResolvedCallback()
    {
        base.OnMalfunctionUnResolvedCallback();
        Debug.Log("Malfunction unresolved");
    }
}