using System;
using UnityEngine;

public class AudioPlayer : MonoBehaviour, IPoolable
{
    [SerializeField]
    private AudioSource audioSource;

    public GameObject ObjectInstance { get; }

    public void SetClipAndPlay(AudioClip clip)
    {
        
    }
    
    public void OnSpawn()
    {
        gameObject.SetActive(true);
        audioSource.Play();
    }

    public void OnRelease()
    {
        gameObject.SetActive(false);
        audioSource.Stop();
    }
}