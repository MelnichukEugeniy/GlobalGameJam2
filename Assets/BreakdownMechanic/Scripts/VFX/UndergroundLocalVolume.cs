using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "VFX/" + nameof(UndergroundLocalVolume), fileName = nameof(UndergroundLocalVolume))]
public class UndergroundLocalVolume : ScriptableObject
{
    [field: SerializeField]
    public Observable<VolumeProfile> Profile { get; private set; } = new ();

    [SerializeField]
    private VolumeProfile fogProfile;

    [SerializeField]
    private VolumeProfile hotTemperatureProfile;
    
    public void SetFog()
    {
        Profile.Value = fogProfile;
        Debug.Log("FOG VOLUME");
    }

    public void SetHotTemperature()
    {
        Profile.Value = hotTemperatureProfile;
        Debug.Log("HOT VOLUME");
    }

    public void ClearEffects()
    {
        Profile.Value = null;
    }

    private void OnEnable()
    {
        ClearEffects();
    }
}