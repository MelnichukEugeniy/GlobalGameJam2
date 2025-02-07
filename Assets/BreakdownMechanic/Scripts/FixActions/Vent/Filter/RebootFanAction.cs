using UnityEngine;

[CreateAssetMenu(fileName = "RebootFan", menuName = "Malfunctions/FixActions/Vent/Fan/RebootFan")]
public class RebootFanAction : FixAction
{
    public override void PerformAction()
    {
        Debug.Log("Fan was rebooted");
    }
}