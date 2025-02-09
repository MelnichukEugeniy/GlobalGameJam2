using UnityEngine;

[CreateAssetMenu(fileName = "EnableEngineCooling", menuName = "Malfunctions/FixActions/Vent/Fan/EnableEngineCooling")]
public class EnableEngineCoolingAction : FixAction
{
    public override void PerformAction()
    {
        Debug.Log("Enabled engine cooling.");
    }
}