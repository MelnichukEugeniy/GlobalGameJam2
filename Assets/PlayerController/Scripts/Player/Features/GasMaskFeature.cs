using UnityEngine;

[CreateAssetMenu(fileName = nameof(GasMaskFeature), menuName = "Player/Features/GasMask")]
public class GasMaskFeature : BasePlayerFeature
{
    [SerializeField]
    private GasMaskConfig config;
    
    [SerializeField]
    private PlayerInput playerInput;

    /// <summary>
    /// State of gasmask. Equipped also mean it effect visible.
    /// </summary>
    public bool Equipped { get; private set; }
    public bool IsFilterWorking { get; private set; }
    
    public float FilterCapacitySecondsLeft { get; private set; }

    public float FilterCapacitySeconds
    {
        get => config.FilterCapacitySeconds;
    }

    public float FilterPercentage
    {
        get => FilterCapacitySecondsLeft / FilterCapacitySeconds;
    }

    private float secondsTimer;

    public override void Update()
    {
        if (playerInput.GasMaskDown())
        {
            FlipFlopEquip();
        }

        if (playerInput.EquipNewFilterDown())
        {
            AttachFilter();
        }
        
        FilterTick();
    }

    private void FilterTick()
    {
        if(IsFilterWorking == false)
            return;
        
        secondsTimer += Time.deltaTime;

        if (secondsTimer < .1f)
            return;
        
        FilterCapacitySecondsLeft -= secondsTimer;
        secondsTimer = 0;
        
        InvokeFilterCapacityLeftChanged();
        
        if (FilterCapacitySecondsLeft <= 0)
        {
            IsFilterWorking = false;
        }
    }

    private void FlipFlopEquip()
    {
        Equipped = !Equipped;
        IsFilterWorking = Equipped;
        
        EventBus<GasMaskEquipChangedEvent>.Raise(new GasMaskEquipChangedEvent(Equipped));
    }

    private void AttachFilter()
    {
        FilterCapacitySecondsLeft = FilterCapacitySeconds;
        
        InvokeFilterCapacityLeftChanged();
    }

    private void InvokeFilterCapacityLeftChanged()
    {
        EventBus<FilterCapacityLeftChangedEvent>.Raise(new FilterCapacityLeftChangedEvent(FilterCapacitySecondsLeft, this, playerController));
    }
}

public struct GasMaskEquipChangedEvent : IEvent
{
    public bool Visibility;

    public GasMaskEquipChangedEvent(bool visibility)
    {
        Visibility = visibility;
    }
}

public struct FilterCapacityLeftChangedEvent : IEvent
{
    public float FilterCapacitySecondsLeft;
    public GasMaskFeature Feature;
    public PlayerController Player;

    public FilterCapacityLeftChangedEvent(float filterCapacitySecondsLeft, GasMaskFeature feature, PlayerController player)
    {
        FilterCapacitySecondsLeft = filterCapacitySecondsLeft;
        Feature = feature;
        Player = player;
    }
}