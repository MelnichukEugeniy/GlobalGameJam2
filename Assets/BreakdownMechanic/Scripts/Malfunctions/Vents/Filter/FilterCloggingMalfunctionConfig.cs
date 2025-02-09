using UnityEngine;

[CreateAssetMenu(fileName = nameof(FilterCloggingMalfunctionConfig), menuName = "Malfunction/Vents/Filter/Config")]
public class FilterCloggingMalfunctionConfig : ScriptableObject
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