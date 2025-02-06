using UnityEngine;

[CreateAssetMenu(fileName = nameof(FilterMalfunctionConfig), menuName = "Malfunction/Vents/Filter/Config")]
public class FanOverpoweredMalfunctionConfig : ScriptableObject
{
    [field: SerializeField]
    public float FanCriticalWorkTimeSecond { get; private set; } = 10 * 60;
    
    
}