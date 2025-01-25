using UnityEngine;

public class InfectionZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<PlayerController>(out var player))
            return;

        EventBus<PlayerEnterInfectionZoneEvent>.Raise(new PlayerEnterInfectionZoneEvent(player));
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<PlayerController>(out var player))
            return;

        EventBus<PlayerExitInfectionZoneEvent>.Raise(new PlayerExitInfectionZoneEvent(player));
    }
}