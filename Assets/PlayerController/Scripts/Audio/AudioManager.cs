using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance
    {
        get
        {
            if (_instance != null)
                return _instance;
            
            throw new NullReferenceException("Add AudioManager to scene.");
        }
        private set
        {
            if (_instance == null)
                _instance = value;
            else
            {
                Destroy(value.gameObject);
                throw new Exception("Audio Manager already exist.");
            }
        }
    }
    private static AudioManager _instance;

    [SerializeField]
    private AudioObjectPoolEntryPoint audiosPoolManager;
    
    private void Awake()
    {
        Instance = this;
        audiosPoolManager.InitializePoolWithParent(transform);
        
        DontDestroyOnLoad(gameObject);
    }

    public AudioPlayer GetAudioPlayer()
    {
        return audiosPoolManager.GetAudioPlayer();
    }
    
    public AudioPlayer PlayAudioClip(AudioClip clip)
    {
        var audioPlayer = audiosPoolManager.GetAudioPlayer();
        audioPlayer.Play(clip);

        return audioPlayer;
    }

    public void ReleaseAudioPlayer(AudioPlayer audioPlayer)
    {
        audiosPoolManager.ReleasePlayer(audioPlayer);
    }
}