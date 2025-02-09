using System;
using System.Linq;
using Systems.Persistence;
using UnityEngine;

[DefaultExecutionOrder(-1000)]
public class VentsMalfunctionController : MalfunctionController<VentMalfunction>, IBind<FanOverpoweredMalfunction.FanData>, IBind<FilterCloggingMalfunction.FilterData>
{
    public static VentsMalfunctionController Instance;
    
    public event Action<VentMalfunction> MalfunctionDetected;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        Malfunctions.ForEach(x => x.Initialize());
    }

    private void InvokeMalfunctionDetected(VentMalfunction malfunction)
    {
        Debug.Log($"DETECTED {malfunction.name}");
        EventBus<MalfunctionDetectedEvent>.Raise(new MalfunctionDetectedEvent() { Malfunction = malfunction});
    }

    public SerializableGuid Id { get; set; }
    private FilterCloggingMalfunction.FilterData filterData;
    private FanOverpoweredMalfunction.FanData fanData;
    
    public void Bind(FilterCloggingMalfunction.FilterData data)
    {
        filterData = data;

        filterData.state.OnChanged += OnFilterStateChanged;
    }

    public void Bind(FanOverpoweredMalfunction.FanData data)
    {
        fanData = data;

        fanData.state.OnChanged += OnFanStateChanged;
    }

    private void OnFilterStateChanged(EMalfunctionState state)
    {
        if(state is EMalfunctionState.Detected)
            InvokeMalfunctionDetected(Malfunctions.FirstOrDefault(x => x is FilterCloggingMalfunction));
    }

    private void OnFanStateChanged(EMalfunctionState state)
    {
        if(state is EMalfunctionState.Detected)
            InvokeMalfunctionDetected(Malfunctions.FirstOrDefault(x => x is FanOverpoweredMalfunction));
    }

    private void OnDestroy()
    {
        if(filterData != null)
            filterData.state.OnChanged -= OnFilterStateChanged;
        if(fanData != null)
            fanData.state.OnChanged -= OnFanStateChanged;
    }
}