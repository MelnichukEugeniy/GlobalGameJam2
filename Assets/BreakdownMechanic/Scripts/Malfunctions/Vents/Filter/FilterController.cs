using System;
using System.Collections.Generic;
using Systems.Persistence;
using UnityEngine;

public class FilterController : MonoBehaviour, IInteractable, ILookInteractable, IBind<FilterCloggingMalfunction.FilterData>
{
    [SerializeField]
    private UndergroundLocalVolume undergroundLocalVolume;
    
    [SerializeField]
    private FilterCloggingMalfunction malfunction;

    [SerializeField]
    private List<FixAction> fixActions;

    private List<FixAction> runtimeFixActions;
    private FilterCloggingMalfunction.FilterData data;

    public SerializableGuid Id { get; set; }

    private void Awake()
    {
        runtimeFixActions = new List<FixAction>(fixActions);
    }

    public void Interact()
    {
        if(data.state.Value != EMalfunctionState.CanBeFixed)
            return;
        
        var performedAction = FixSelectionsListWidget.Instance.PerformSelectedAction();
        if (performedAction is TakeOffFilterAction)
        {
            runtimeFixActions.Remove(performedAction);
            if(FixSelectionsListWidget.Instance.IsActive)
                FixSelectionsListWidget.Instance.ShowSelectionsList(runtimeFixActions);
            return;
        }

        if (performedAction is ClearFilterAction)
        {
            if (runtimeFixActions.Exists(x => x is TakeOffFilterAction))
                return;
            runtimeFixActions.Remove(performedAction);
            if(FixSelectionsListWidget.Instance.IsActive)
                FixSelectionsListWidget.Instance.ShowSelectionsList(runtimeFixActions);
        }

        if (performedAction is PlaceFilterAction)
        {
            if(runtimeFixActions.Exists(x=> x is TakeOffFilterAction || x is ClearFilterAction))
                return;
                
            runtimeFixActions.Clear();
            runtimeFixActions.AddRange(fixActions);
            malfunction.Fix();
            FixSelectionsListWidget.Instance.Hide();
        }
    }

    public void OnLookEnter()
    {
        if(data.state.Value is EMalfunctionState.CanBeFixed)
            FixSelectionsListWidget.Instance.ShowSelectionsList(runtimeFixActions);
    }

    public void OnLookExit()
    {
        FixSelectionsListWidget.Instance.Hide();
    }
    
    public void Bind(FilterCloggingMalfunction.FilterData data)
    {
        this.data = data;
        Id = data.Id;
        
        data.cloggingValue.OnChanged += OnCloggingValueChanged;
        data.state.OnChanged += OnMalfunctionStateChanged;
        Debug.Log($"Bind data for {GetType().Name}");
    }

    private void OnMalfunctionStateChanged(EMalfunctionState obj)
    {
        if (obj == EMalfunctionState.Detected)
        {
            
        }
    }

    private void OnCloggingValueChanged(float f)
    {
        if (malfunction.IsMalfunctionDetected())
        {
            undergroundLocalVolume.SetFog();
        } 
    }

    private void OnDestroy()
    {
        data.cloggingValue.OnChanged -= OnCloggingValueChanged;
        data.state.OnChanged -= OnMalfunctionStateChanged;
    }
}