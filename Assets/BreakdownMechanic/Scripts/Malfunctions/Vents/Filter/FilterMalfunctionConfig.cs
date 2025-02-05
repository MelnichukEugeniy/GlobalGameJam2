using UnityEngine;

[CreateAssetMenu(fileName = nameof(FilterMalfunctionConfig), menuName = "Malfunction/Vents/Filter/Config")]
public class FilterMalfunctionConfig : ScriptableObject
{
    [field: SerializeField]
    public float CloggingCriticalValue { get; private set; }
    
    [field: SerializeField]
    public float CloggingRate { get; private set; }
    
    [field: SerializeField]
    public float CloggingTickSeconds { get; private set; }
    
    [field: SerializeField]
    public float CloggingPerTick { get; private set; }
}