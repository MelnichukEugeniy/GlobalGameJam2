using System;
using Systems.Persistence;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(FilterCloggingMalfunction), menuName = "Malfunction/Vents/Fan/FanOverpowered")]
public class FanOverpoweredMalfunction : VentMalfunction, IBind<FanOverpoweredMalfunction.FanData>
{
    public SerializableGuid Id { get; set; } = SerializableGuid.NewGuid();
    
    private float WorkingTimeSeconds
    {
        get => data.workingTimeSeconds.Value;
        set
        {
            data.workingTimeSeconds.Value = value;
            UpdateTemperature();
            if (IsMalfunctionDetected() && data.state.Value == EMalfunctionState.Undetected)
            {
                data.state.Value = EMalfunctionState.Detected;
                InvokeMalfunctionDetected();
            }
        }
    }
    
    [field: SerializeField]
    public FanOverpoweredMalfunctionConfig Config { get; private set; }

    [SerializeField]
    private UndergroundLocalVolume undergroundLocalVolume;
    
    private Timer timer;
    private FanData data;
    
    public override void Initialize()
    { 
        timer = Timer.SecondsTimer;
        timer.OnTimeout += OnSecondsTimerTimeout;
        
        Debug.Log($"Initialized {GetType().Name}");
    }

    public override void Update()
    {
    }

    public override bool IsMalfunctionDetected()
    {
        return data.temperature.Value >= Config.CriticalTemperatureCelsius;
    }

    public override void Fix()
    {
        WorkingTimeSeconds = 0;
        undergroundLocalVolume.ClearEffects();
        data.state.Value = EMalfunctionState.Undetected;
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
    
    private void UpdateTemperature()
    {
        data.temperature.Value = data.workingTimeSeconds.Value.Map(0, Config.CriticalWorkTimeSecond, Config.MinTemperatureCelsius, Config.MaxTemperatureCelsius);
    }
    
    private void OnSecondsTimerTimeout(Timer _)
    {
        Debug.Log("ADD SECOND");
        WorkingTimeSeconds += 1;
    }
    
    public override void Dispose()
    {
        timer.OnTimeout -= OnSecondsTimerTimeout;
    }
    
    [Serializable]
    public class FanData : ISaveable
    {
        public SerializableGuid Id { get; set; }

        public Observable<EMalfunctionState> state = new Observable<EMalfunctionState>();
        public Observable<float> workingTimeSeconds = new Observable<float>();
        public Observable<float> temperature = new Observable<float>();
    }

    public void Bind(FanData data)
    {
        this.data = data;
        this.data.Id = Id;
        
        Debug.Log($"Bind data for {GetType().Name}");
    }
}