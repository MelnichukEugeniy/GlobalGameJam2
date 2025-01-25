public struct PlayerDeadEvent : IEvent
{
    public PlayerController Player;

    public PlayerDeadEvent(PlayerController player)
    {
        Player = player;
    }
}