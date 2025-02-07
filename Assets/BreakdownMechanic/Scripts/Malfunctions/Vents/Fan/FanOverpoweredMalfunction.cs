using System;
using Systems.Persistence;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(FilterCloggingMalfunction), menuName = "Malfunction/Vents/Fan/FanOverpowered")]
public class FanOverpoweredMalfunction : VentMalfunction, IBind<FanOverpoweredMalfunction.FanData>
{
    public SerializableGuid Id { get; set; } = SerializableGuid.NewGuid();
    
    private float WorkingTimeSeconds
    {
        get => data.workingTimeSeconds;
        set
        {
            data.workingTimeSeconds = value;
            UpdateTemperature();
            data.InvokeOnChange();
        }
    }
    
    [SerializeField]
    private FanOverpoweredMalfunctionConfig config;
    private Timer timer;
    private FanData data;
    
    public override void Initialize()
    { 
        timer = Timer.SecondsTimer;
        timer.OnTimeout += OnSecondsTimerTimeout;
    }

    public override void Update()
    {
    }

    public override bool IsMalfunctionDetected()
    {
        return data.temperature >= config.CriticalTemperatureCelsius;
    }

    private void UpdateTemperature()
    {
        data.temperature = data.workingTimeSeconds.Map(0, config.CriticalWorkTimeSecond, config.MinTemperatureCelsius, config.MaxTemperatureCelsius);
    }
    
    private void OnSecondsTimerTimeout(Timer _)
    {
        WorkingTimeSeconds += 1;
    }
    
    public override void Dispose()
    {
        timer.OnTimeout -= OnSecondsTimerTimeout;
    }
    
    [Serializable]
    public class FanData : ISaveable
    {
        public event Action OnChange; 

        public SerializableGuid Id { get; set; }

        public float workingTimeSeconds;
        public float temperature;

        public void InvokeOnChange()
        {
            OnChange?.Invoke();
        }
    }

    public void Bind(FanData data)
    {
        this.data = data;
        this.data.Id = Id;
    }
}