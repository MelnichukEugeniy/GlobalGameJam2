using UnityEngine;

public abstract class BasePlayerFeature : ScriptableObject
{
    protected PlayerController playerController;
    
    public virtual void InitializeWithPlayer(PlayerController player)
    {
        playerController = player;
    }
    
    public virtual void Update() {}
}