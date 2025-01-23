using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(InfectionFeature), menuName = "Player/Features/Infection")]
public class InfectionFeature : BasePlayerFeature
{
    [SerializeField]
    private InfectionConfig config;
    
    private HealthFeature healthFeature;
    private GasMaskFeature gasMaskFeature;

    private Coroutine _infectionCoroutine;
    
    private EventBinding<PlayerEnterInfectionZoneEvent> playerEnterInfectionZoneEventBinding;
    private EventBinding<PlayerExitInfectionZoneEvent> playerExitInfectionZoneEventBinding;

    public bool InInfectionZone { get; private set; }

    public override void InitializeWithPlayer(PlayerController player)
    {
        base.InitializeWithPlayer(player);

        healthFeature = player.GetFeature<HealthFeature>();
        gasMaskFeature = player.GetFeature<GasMaskFeature>();
        
        playerEnterInfectionZoneEventBinding = new EventBinding<PlayerEnterInfectionZoneEvent>(OnPlayerEnterInfectionZone);
        playerExitInfectionZoneEventBinding = new EventBinding<PlayerExitInfectionZoneEvent>(OnPlayerExitInfectionZone);
        
        EventBus<PlayerEnterInfectionZoneEvent>.Register(playerEnterInfectionZoneEventBinding);
        EventBus<PlayerExitInfectionZoneEvent>.Register(playerExitInfectionZoneEventBinding);
    }

    private void OnPlayerEnterInfectionZone(PlayerEnterInfectionZoneEvent @event)
    {
        if(InInfectionZone)
            return;

        InInfectionZone = true;
        if (gasMaskFeature.Equipped == false)
        {
            StartInfection();
        }
        Debug.Log("Player Entered Infected Zone");
    }

    private void OnPlayerExitInfectionZone(PlayerExitInfectionZoneEvent @event)
    {
        if(InInfectionZone == false)
            return;

        StopInfection();
        Debug.Log("Player Left Infected Zone");
    }

    private void StartInfection()
    {
        _infectionCoroutine = playerController.StartCoroutine(InfectionTickRoutine());
    }

    private void StopInfection()
    {
        if(_infectionCoroutine != null)
            playerController.StopCoroutine(_infectionCoroutine);

        InInfectionZone = false;
    }

    private IEnumerator InfectionTickRoutine()
    {
        while (InInfectionZone)
        {
            yield return new WaitForSeconds(config.PeriodInSeconds);
            
            healthFeature.TakeDamage(config.PeriodicalInfectionDamage);
        }
    }
    
    public override void Dispose()
    {
        base.Dispose();
    
        EventBus<PlayerEnterInfectionZoneEvent>.Deregister(playerEnterInfectionZoneEventBinding);
        EventBus<PlayerExitInfectionZoneEvent>.Deregister(playerExitInfectionZoneEventBinding);
    }
}