using UnityEngine;

[CreateAssetMenu(fileName = "PlaceFilter", menuName = "Malfunctions/FixActions/Vent/Filter/PlaceFilter")]
public class PlaceFilterAction : FixAction
{
    public override void PerformAction()
    {
        Debug.Log("Filter was placed back");
    }
}
