using UnityEngine;

[CreateAssetMenu(fileName = "TakeOffFilter", menuName = "Malfunctions/FixActions/Vent/Filter/TakeOffFilter")]
public class TakeOffFilterAction : FixAction
{
    public override void PerformAction()
    {
        Debug.Log("Filter has been taken off");
    }
}
