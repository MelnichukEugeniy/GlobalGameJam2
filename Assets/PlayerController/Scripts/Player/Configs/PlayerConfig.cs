using UnityEngine;

[CreateAssetMenu(fileName = nameof(PlayerConfig), menuName = "Player/" + nameof(PlayerConfig))]
public class PlayerConfig : ScriptableObject
{
    [field: SerializeField]
    public float BaseHealth { get; private set; } 
    [field: SerializeField]
    public float MaxHealth { get; private set; }
}