using UnityEngine;

public class AudioPlayer : MonoBehaviour, IPoolable
{
    [SerializeField]
    private AudioSource audioSource;

    public GameObject ObjectInstance { get; }

    public void Play(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void Stop()
    {
        audioSource.Stop();
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

    public void SetLoop(bool loop)
    {
        audioSource.loop = loop;
    }
}