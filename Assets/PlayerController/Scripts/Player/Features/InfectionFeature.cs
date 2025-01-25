using System;
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
    private EventBinding<GasMaskEquipChangedEvent> gasMaskEquipChangedEventBinding;
    private EventBinding<FilterCapacityLeftChangedEvent> filterCapacityLeftChangedEventBinding;

    public bool InInfectionZone { get; private set; }

    public override void InitializeWithPlayer(PlayerController player)
    {
        base.InitializeWithPlayer(player);

        healthFeature = player.GetFeature<HealthFeature>();
        gasMaskFeature = player.GetFeature<GasMaskFeature>();
        
        playerEnterInfectionZoneEventBinding = new EventBinding<PlayerEnterInfectionZoneEvent>(OnPlayerEnterInfectionZone);
        playerExitInfectionZoneEventBinding = new EventBinding<PlayerExitInfectionZoneEvent>(OnPlayerExitInfectionZone);
        gasMaskEquipChangedEventBinding = new EventBinding<GasMaskEquipChangedEvent>(OnGasMaskEquipChanged);
        filterCapacityLeftChangedEventBinding = new EventBinding<FilterCapacityLeftChangedEvent>(OnFilterCapacityLeftChanged);
        
        EventBus<PlayerEnterInfectionZoneEvent>.Register(playerEnterInfectionZoneEventBinding);
        EventBus<PlayerExitInfectionZoneEvent>.Register(playerExitInfectionZoneEventBinding);
        EventBus<GasMaskEquipChangedEvent>.Register(gasMaskEquipChangedEventBinding);
        EventBus<FilterCapacityLeftChangedEvent>.Register(filterCapacityLeftChangedEventBinding);
    }

    private void OnPlayerEnterInfectionZone(PlayerEnterInfectionZoneEvent @event)
    {
        if(InInfectionZone)
            return;

        InInfectionZone = true;
        if (gasMaskFeature.Equipped == false)
        {
            StartInfectionTick();
        }
    }

    private void OnPlayerExitInfectionZone(PlayerExitInfectionZoneEvent @event)
    {
        if(InInfectionZone == false)
            return;
        
        InInfectionZone = false;
        
        StopInfectionTick();
    }

    private void OnGasMaskEquipChanged(GasMaskEquipChangedEvent @event)
    {
        if (!InInfectionZone)
            return;
        
        if (gasMaskFeature.Equipped)
        {
            StopInfectionTick();
        }
        else
        {
            StartInfectionTick();
        }
    }

    private void OnFilterCapacityLeftChanged(FilterCapacityLeftChangedEvent @event)
    {
        if (@event.Feature.IsFilterWorking == false)
        {
            StartInfectionTick();
        }
    }

    private void StartInfectionTick()
    {
        _infectionCoroutine = playerController.StartCoroutine(InfectionTickRoutine());
    }

    private void StopInfectionTick()
    {
        if(_infectionCoroutine != null)
            playerController.StopCoroutine(_infectionCoroutine);
    }

    private IEnumerator InfectionTickRoutine()
    {
        while (InInfectionZone && gasMaskFeature.Equipped == false)
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
        EventBus<GasMaskEquipChangedEvent>.Deregister(gasMaskEquipChangedEventBinding);
        EventBus<FilterCapacityLeftChangedEvent>.Deregister(filterCapacityLeftChangedEventBinding);
    }
}