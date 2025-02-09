using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MalfunctionResolveBase<T> : Widget where T : MalfunctionBase
{
    public Type MalfunctionType
    {
        get => malfunction.GetType();
    }

    [SerializeField]
    protected T malfunction;

    [SerializeField]
    private int resolveToggleIndex;
    [SerializeField]
    protected List<Toggle> reasonToggles;

    private int currentToggleIndex;

    public override void Show()
    {
        base.Show();
        
        reasonToggles.ForEach(x =>
        {
            x.onValueChanged.RemoveAllListeners();
            x.onValueChanged.AddListener(isOn => OnToggleChangedValue(reasonToggles.IndexOf(x), isOn));
        });
    }

    private void OnToggleChangedValue(int index, bool isOn)
    {
        Debug.Log($"index {index} enabled {isOn}");
        reasonToggles[currentToggleIndex].SetIsOnWithoutNotify(false);
        currentToggleIndex = index;
        reasonToggles[index].SetIsOnWithoutNotify(isOn);

        if (index == resolveToggleIndex)
            OnMalfunctionResolvedCallback();
        else
            OnMalfunctionUnResolvedCallback();
    }

    protected virtual void OnMalfunctionResolvedCallback()
    {
        
    }
    
    protected virtual void OnMalfunctionUnResolvedCallback()
    {
        
    }
}