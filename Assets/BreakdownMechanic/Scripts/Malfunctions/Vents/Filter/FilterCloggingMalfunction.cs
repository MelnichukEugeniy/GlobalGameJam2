using System;
using Systems.Persistence;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(FilterCloggingMalfunction), menuName = "Malfunction/Vents/Filter/Clogging")]
public class FilterCloggingMalfunction : VentMalfunction, IBind<FilterCloggingMalfunction.FilterData>
{
    public SerializableGuid Id { get; set; } = SerializableGuid.NewGuid();
    [SerializeField]
    private FilterMalfunctionConfig config;

    private float cloggingValue
    {
        get => data.cloggingValue;
        set
        {
            data.cloggingValue = value;
            data.InvokeOnChange();
        }
    }
    
    private FilterData data;
    private Timer cloggingTimer;

    [Serializable]
    public class FilterData : ISaveable
    {
        public event Action OnChange;
        
        public SerializableGuid Id { get; set; }
        public float cloggingValue;

        public void InvokeOnChange()
        {
            OnChange?.Invoke();
        }
    }

    public override void Initialize()
    {
        cloggingTimer = Timer.CreateTimer(config.CloggingTickSeconds, config.CloggingRate, true);
        cloggingTimer.OnTimeout += OnCloggingTimerTimeout;
    }
    
    public override void Update()
    {
        
    }

    public override bool IsMalfunctionDetected()
    {
        return cloggingValue >= config.CloggingCriticalValue;
    }
    
    private void OnCloggingTimerTimeout(Timer timer)
    {
        cloggingValue += config.CloggingPerTick;
    }

    public override void Dispose()
    {
        cloggingTimer.Dispose();
    }

    
    public void Bind(FilterData data)
    {
        this.data = data;
        this.data.Id = Id;
    }
}