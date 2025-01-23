using UnityEngine;

[CreateAssetMenu(fileName = nameof(GasMaskFeature), menuName = "Player/Features/GasMask")]
public class GasMaskFeature : BasePlayerFeature
{
    [SerializeField]
    private GasMaskConfig config;
    
    [SerializeField]
    private PlayerInput playerInput;

    /// <summary>
    /// Show state of gasmask. Equipped also mean it effect visible.
    /// </summary>
    public bool Equipped { get; private set; }
    
    public float FilterCapacitySecondsLeft { get; private set; }

    public float FilterCapacitySeconds
    {
        get => config.FilterCapacitySeconds;
    }

    public float FilterPercentage
    {
        get => Mathf.InverseLerp(0, FilterCapacitySeconds, FilterCapacitySecondsLeft);
    }

    private float _secondsTimer;

    public override void Update()
    {
        if (playerInput.GasMaskDown())
        {
            FlipFlopEquip();
        }
        
        FilterTick();
    }

    private void FilterTick()
    {
        _secondsTimer += Time.deltaTime;

        if (_secondsTimer > 1f)
        {
            FilterCapacitySecondsLeft -= _secondsTimer;
        }

        if (FilterCapacitySecondsLeft <= 0)
        {
            
        }
    }

    private void FlipFlopEquip()
    {
        Equipped = !Equipped;
        EventBus<GasMaskEquipChanged>.Raise(new GasMaskEquipChanged(Equipped));
    }

    private void InvokeFilterCapacityLeftChanged()
    {
        EventBus<FilterCapacityLeftChangedEvent>.Raise(new FilterCapacityLeftChangedEvent(FilterCapacitySecondsLeft, this, playerController));
    }
}

public struct GasMaskEquipChanged : IEvent
{
    public bool Visibility;

    public GasMaskEquipChanged(bool visibility)
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