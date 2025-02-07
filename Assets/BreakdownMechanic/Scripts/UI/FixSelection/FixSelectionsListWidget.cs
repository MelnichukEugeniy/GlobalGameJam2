using System.Collections.Generic;
using UnityEngine;

public class FixSelectionsListWidget : Widget
{
    [SerializeField]
    private List<SelectionWidget> selectionWidgets;

    private List<FixAction> selectionActions;

    private int currentSelectionIndex;

    public static FixSelectionsListWidget Instance;
    
    protected override void Awake()
    {
        Instance = this;
        base.Awake();
        selectionActions = new List<FixAction>();
    }

    public void ShowSelectionsList(List<FixAction> fixActionSelections)
    {
        selectionActions.Clear();
        selectionActions.AddRange(fixActionSelections);

        for (int i = 0; i < selectionWidgets.Count; i++)
        {
            if (i >= selectionActions.Count)
            {
                selectionWidgets[i].Hide();
                continue;
            }
            selectionWidgets[i].WithText(selectionActions[i].Text);
            selectionWidgets[i].Show();
        }
        currentSelectionIndex = 0;
        SetActiveCurrentSelection();
        
        Show();
    }

    private void Update()
    {
        var scrollDelta = Input.GetAxisRaw("Mouse ScrollWheel");
        
        if(scrollDelta != 0)
        {
            if (scrollDelta > 0)
            {
                PreviousSelection();
            }
            else if(scrollDelta < 0)
            {
                NextSelection();
            }
        }
    }

    private void SetActiveCurrentSelection()
    {
        for (int i = 0; i < selectionWidgets.Count; i++)
        {
            if (i != currentSelectionIndex)
            {
                selectionWidgets[i].Disactivate();
                continue;
            }

            selectionWidgets[i].Activate();
        }
    }

    private void NextSelection()
    {
        currentSelectionIndex++;
        if (currentSelectionIndex == selectionActions.Count)
        {
            currentSelectionIndex = 0;
        }
        SetActiveCurrentSelection();
    }

    private void PreviousSelection()
    {
        currentSelectionIndex--;
        if (currentSelectionIndex < 0)
        {
            currentSelectionIndex = selectionActions.Count - 1;
        }
        SetActiveCurrentSelection();
    }

    public void PerformSelectedAction()
    {
        if(!content.gameObject.activeSelf)
            return;
        
        selectionActions[currentSelectionIndex].PerformAction();
    }
}