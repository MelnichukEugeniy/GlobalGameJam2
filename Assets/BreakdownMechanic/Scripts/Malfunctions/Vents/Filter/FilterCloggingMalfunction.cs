using System;
using Systems.Persistence;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(FilterCloggingMalfunction), menuName = "Malfunction/Vents/Filter/Clogging")]
public class FilterCloggingMalfunction : VentMalfunction, IBind<FilterCloggingMalfunction.FilterData>
{
    public SerializableGuid Id { get; set; } = SerializableGuid.NewGuid();
    
    [field: SerializeField]
    public FilterCloggingMalfunctionConfig Config { get; private set; }
    [SerializeField]
    private UndergroundLocalVolume undergroundLocalVolume;

    private float cloggingValue
    {
        get => data.cloggingValue.Value;
        set
        {
            data.cloggingValue.Value = value;
            if (IsMalfunctionDetected() && data.state.Value == EMalfunctionState.Undetected)
            {
                data.state.Value = EMalfunctionState.Detected;
            }
        }
    }
    
    private FilterData data;
    private Timer cloggingTimer;

    [Serializable]
    public class FilterData : ISaveable
    {
        public SerializableGuid Id { get; set; }
        public Observable<EMalfunctionState> state = new Observable<EMalfunctionState>();
        public Observable<float> cloggingValue = new Observable<float>();
    }

    public override void Initialize()
    {
        cloggingTimer = Timer.CreateTimer(Config.CloggingTickSeconds, Config.CloggingRate, true);
        cloggingTimer.OnTimeout += OnCloggingTimerTimeout;
        
        Debug.Log($"Initialize {GetType().Name}");
    }
    
    public override void Update()
    {
        
    }

    public override bool IsMalfunctionDetected()
    {
        return cloggingValue >= Config.CloggingCriticalValue;
    }

    public override void Fix()
    {
        cloggingValue = 0;
        data.state.Value = EMalfunctionState.Undetected;
        undergroundLocalVolume.ClearEffects();
    }
    
    public override void Show()
    {
        data.state.Value = EMalfunctionState.Detected;
    }

    public override void Hide()
    {
        data.state.Value = EMalfunctionState.Hided;
    }
    
    public override void Solve()
    {
        data.state.Value = EMalfunctionState.CanBeFixed;
    }

    private void OnCloggingTimerTimeout(Timer timer)
    {
        cloggingValue += Config.CloggingPerTick;
        Debug.Log("FILTER CLOGGING");
    }

    public override void Dispose()
    {
        cloggingTimer.Dispose();
    }

    public void Bind(FilterData data)
    {
        this.data = data;
        this.data.Id = Id;
        
        Debug.Log($"Bind data for {GetType().Name}");
    }
}