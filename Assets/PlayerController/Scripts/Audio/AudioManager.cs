using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioObjectPoolEntryPoint audiosPoolManager;
    
    private void Awake()
    {
        audiosPoolManager.InitializePoolWithParent(transform);
    }

    public void PlayAudioClip(AudioClip clip)
    {
        
    }
}