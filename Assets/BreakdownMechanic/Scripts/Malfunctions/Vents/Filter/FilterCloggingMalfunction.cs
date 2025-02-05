using UnityEngine;

[CreateAssetMenu(fileName = nameof(FilterCloggingMalfunction), menuName = "Malfunction/Vents/Filter/Clogging")]
public class FilterCloggingMalfunction : VentMalfunction
{
    [SerializeField]
    private FilterMalfunctionConfig config;

    private float cloggingValue = 0;
    private Timer cloggingTimer;

    public override void Initialize()
    {
        cloggingTimer = Timer.CreateTimer(config.CloggingTickSeconds, config.CloggingRate, true);
        cloggingTimer.OnTimeout += OnCloggingTimerTimeout;
    }
    
    public override void Update()
    {
        
    }

    private void OnCloggingTimerTimeout(Timer timer)
    {
        cloggingValue += config.CloggingPerTick;
    }

    public override void Dispose()
    {
        cloggingTimer.Dispose();
    }
}