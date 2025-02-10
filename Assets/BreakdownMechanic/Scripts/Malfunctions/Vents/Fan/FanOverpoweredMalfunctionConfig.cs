using UnityEngine;

[CreateAssetMenu(fileName = nameof(FilterCloggingMalfunctionConfig), menuName = "Malfunction/Vents/Fan/OverpoweredConfig")]
public class FanOverpoweredMalfunctionConfig : ScriptableObject
{
    [field: SerializeField]
    public float CriticalWorkTimeSecond { get; private set; } = 10 * 60;

    [field: SerializeField]
    public float CriticalTemperatureCelsius { get; private set; } = 90;

    [field: SerializeField]
    public float MinTemperatureCelsius { get; private set; } = 20;

    [field: SerializeField]
    public float MaxTemperatureCelsius { get; private set; } = 100;
}