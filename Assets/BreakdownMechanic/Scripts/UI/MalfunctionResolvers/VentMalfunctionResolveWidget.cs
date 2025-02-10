using System;
using UnityEngine;

public class VentMalfunctionResolveWidget : MalfunctionResolveBase<VentMalfunction>
{
    [field: SerializeField]
    public string InstructionText;
    
    public event Action<VentMalfunctionResolveWidget, bool> MalfunctionResolved;
    protected override void OnMalfunctionResolvedCallback()
    {
        base.OnMalfunctionResolvedCallback();
        MalfunctionResolved?.Invoke(this, true);
        
        reasonToggles.ForEach(x => x.onValueChanged.RemoveAllListeners());
    }

    protected override void OnMalfunctionUnResolvedCallback()
    {
        base.OnMalfunctionUnResolvedCallback();
        MalfunctionResolved?.Invoke(this, false);
        
        reasonToggles.ForEach(x => x.onValueChanged.RemoveAllListeners());
    }
}