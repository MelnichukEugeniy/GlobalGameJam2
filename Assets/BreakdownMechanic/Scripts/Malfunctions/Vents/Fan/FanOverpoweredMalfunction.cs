using System;
using Systems.Persistence;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(FilterCloggingMalfunction), menuName = "Malfunction/Vents/Filter/Clogging")]
public class FanOverpoweredMalfunction : VentMalfunction, IBind<FanOverpoweredMalfunction.FanData>
{
    public SerializableGuid Id { get; set; } = SerializableGuid.NewGuid();
    
    private float workingTimeSeconds
    {
        get => data.workingTimeSeconds;
        set
        {
            data.workingTimeSeconds = value;
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
        return false;
    }

    private void OnSecondsTimerTimeout(Timer timer)
    {
        workingTimeSeconds += 1;
    }
    
    public override void Dispose()
    {
        timer.OnTimeout -= OnSecondsTimerTimeout;
    }
    
    public class FanData : ISaveable
    {
        public event Action OnChange; 

        public SerializableGuid Id { get; set; }

        public float workingTimeSeconds;

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