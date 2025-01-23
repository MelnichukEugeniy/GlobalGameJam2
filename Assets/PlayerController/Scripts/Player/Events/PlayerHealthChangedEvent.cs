public struct PlayerHealthChangedEvent : IEvent
{
    /// <summary>
    /// Changed player health value. 
    /// </summary>
    public float Health;

    /// <summary>
    /// Health feature
    /// </summary>
    public HealthFeature Feature;
    
    /// <summary>
    /// Health owner player controller.
    /// </summary>
    public PlayerController Player;
    
    public PlayerHealthChangedEvent(float health, HealthFeature feature, PlayerController player)
    {
        Health = health;
        Feature = feature;
        Player = player;
    }
}