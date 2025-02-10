public struct PlayerEnterInfectionZoneEvent : IEvent
{
    public PlayerController Player;

    public PlayerEnterInfectionZoneEvent(PlayerController player)
    {
        Player = player;
    }
}