using UnityEngine;

public class GasMaskAudioController : MonoBehaviour
{
    [SerializeField]
    private GasMaskConfig gasMaskConfig;
    
    private bool gasMaskEquipped;
    private bool isInInfectionZone;
    private float filterPercentage;
    private bool stateModified;

    private EventBinding<GasMaskEquipChangedEvent> gasMaskEquipChangedEventBinding;
    private EventBinding<FilterCapacityLeftChangedEvent> filterCapacityLeftChangedEventBinding;

    private EventBinding<PlayerEnterInfectionZoneEvent> playerEnterInfectionZoneEventBinding;
    private EventBinding<PlayerExitInfectionZoneEvent> playerExitInfectionZoneEventBinding;

    private AudioClip currentClip;
    private AudioPlayer gasMaskAudioPlayer;
    
    private void Awake()
    {
        gasMaskEquipChangedEventBinding = new EventBinding<GasMaskEquipChangedEvent>(OnGasMaskEquipChanged);
        filterCapacityLeftChangedEventBinding = new EventBinding<FilterCapacityLeftChangedEvent>(OnFilterCapacityChangedEvent);

        playerEnterInfectionZoneEventBinding = new EventBinding<PlayerEnterInfectionZoneEvent>(OnEnterInfectionZone);
        playerExitInfectionZoneEventBinding = new EventBinding<PlayerExitInfectionZoneEvent>(OnExitInfectionZone);
        
        EventBus<GasMaskEquipChangedEvent>.Register(gasMaskEquipChangedEventBinding);
        EventBus<FilterCapacityLeftChangedEvent>.Register(filterCapacityLeftChangedEventBinding);
        
        EventBus<PlayerEnterInfectionZoneEvent>.Register(playerEnterInfectionZoneEventBinding);
        EventBus<PlayerExitInfectionZoneEvent>.Register(playerExitInfectionZoneEventBinding);
    }

    private void Start()
    {
        gasMaskAudioPlayer = AudioManager.Instance.GetAudioPlayer();
        gasMaskAudioPlayer.SetLoop(true);
    }

    private void OnGasMaskEquipChanged(GasMaskEquipChangedEvent @event)
    {
        gasMaskEquipped = @event.Visibility;

        UpdateAudio();
    }
    
    private void OnFilterCapacityChangedEvent(FilterCapacityLeftChangedEvent @event)
    {
        filterPercentage = @event.Feature.FilterPercentage;
        
        UpdateAudio();
    }

    private void OnEnterInfectionZone(PlayerEnterInfectionZoneEvent @event)
    {
        isInInfectionZone = true;
        
        UpdateAudio();
    }

    private void OnExitInfectionZone(PlayerExitInfectionZoneEvent @event)
    {
        isInInfectionZone = false;
        
        UpdateAudio();
    }

    private void UpdateAudio()
    {
        if (gasMaskEquipped)
        {
            if (filterPercentage <= 0)
            {
                TryPlayAudioClip(gasMaskConfig.LackOfBreathingWithGasMaskAudio);
                return;
            }
            
            if (filterPercentage <= gasMaskConfig.FilterBarelyEndPercentage)
            {
                TryPlayAudioClip(gasMaskConfig.HardBreathingWithGasMaskAudio);
                return;
            }
            
            TryPlayAudioClip(gasMaskConfig.NormalBreathingWithGasMaskAudio);
            return;
        }

        if (!isInInfectionZone)
        {
            StopAudio();
            return;
        }
        
        TryPlayAudioClip(gasMaskConfig.BreathingToxinsWithoutGasMask);
    }

    private void TryPlayAudioClip(AudioClip audioClip)
    {
        if(currentClip == audioClip)
            return;
        gasMaskAudioPlayer.Play(audioClip);
        currentClip = audioClip;
    }

    private void StopAudio()
    {
        gasMaskAudioPlayer.Stop();
        currentClip = null;
    }
}