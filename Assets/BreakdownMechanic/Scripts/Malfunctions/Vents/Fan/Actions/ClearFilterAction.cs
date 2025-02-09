using UnityEngine;

[CreateAssetMenu(fileName = "ClearFilter", menuName = "Malfunctions/FixActions/Vent/Filter/ClearFiler")]
public class ClearFilterAction : FixAction
{
    public override void PerformAction()
    {
        Debug.Log("Filter was cleared");
    }
}