public struct PlayerExitInfectionZoneEvent : IEvent
{
    public PlayerController Player;

    public PlayerExitInfectionZoneEvent(PlayerController player)
    {
        Player = player;
    }
}