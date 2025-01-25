using UnityEngine;

[CreateAssetMenu(fileName = nameof(InfectionConfig), menuName = "Infection/Config")]
public class InfectionConfig : ScriptableObject
{
    [field: SerializeField]
    public float PeriodicalInfectionDamage { get; private set; } = 2.5f;

    [field: SerializeField]
    public float PeriodInSeconds { get; private set; } = 1f;
}