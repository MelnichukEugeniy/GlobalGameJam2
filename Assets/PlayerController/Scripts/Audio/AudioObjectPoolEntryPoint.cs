using System;
using UnityEngine;
using UnityEngine.Pool;

public class AudioObjectPoolEntryPoint : MonoBehaviour
{
    [SerializeField]
    private ObjectPoolParams audioObjectParam;

    [SerializeField]
    private AudioPlayer audioPlayerPrefab;

    private Transform audiosParent;
    private ObjectPoolManager poolManager;
    private IObjectPool<AudioPlayer> audioSourcesPool;

    public void InitializePoolWithParent(Transform parent)
    {
        audiosParent = parent;

        poolManager = new ObjectPoolManager();
        
        bool success = poolManager.CreatePool(AudioSourceFactory, audioObjectParam, out var pool);
        
        if(success)
            audioSourcesPool = pool;
        else
            Debug.LogError("Audio pool already exist.");
    }

    public AudioPlayer GetAudioPlayer()
    {
        return audioSourcesPool.Get();
    }

    public void ReleasePlayer(AudioPlayer audioPlayer)
    {
        audioSourcesPool.Release(audioPlayer);
    }

    private AudioPlayer AudioSourceFactory()
    {
        return Instantiate(audioPlayerPrefab, audiosParent);
    }
}