using UnityEngine;
using UnityEngine.Rendering;

public class LocalVignetteController : MonoBehaviour
{
    [SerializeField]
    private Volume volume;

    [SerializeField]
    private UndergroundLocalVolume undergroundLocalVolume;

    private void Awake()
    {
        OnVolumeChanged(undergroundLocalVolume.Profile.Value);
        undergroundLocalVolume.Profile.OnChanged += OnVolumeChanged;
    }

    private void OnVolumeChanged(VolumeProfile profile)
    {
        volume.profile = profile;
    }

    private void OnDestroy()
    {
        undergroundLocalVolume.Profile.OnChanged -= OnVolumeChanged;
    }
}
