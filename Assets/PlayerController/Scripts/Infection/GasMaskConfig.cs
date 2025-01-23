using UnityEngine;

[CreateAssetMenu(fileName = nameof(GasMaskConfig), menuName = "Infection/" + nameof(GasMaskConfig))]
public class GasMaskConfig : ScriptableObject
{
    [field: SerializeField]
    public float FilterCapacitySeconds { get; private set; } = 200;

    [field: SerializeField]
    public float FilterBarelyEndPercentage { get; private set; } = .2f;
    
    [field: SerializeField]
    public AudioClip NormalBreathingWithGasMaskAudio { get; private set; }
    
    [field: SerializeField]
    public AudioClip HardBreathingWithGasMaskAudio { get; private set; }
    
    [field: SerializeField]
    public AudioClip LackOfBreathingWithGasMaskAudio { get; private set; }
    
    [field: SerializeField]
    public AudioClip BreathingToxinsWithoutGasMask { get; private set; }
}