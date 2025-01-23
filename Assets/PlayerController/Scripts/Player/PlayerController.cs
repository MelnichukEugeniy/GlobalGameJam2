using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    private PlayerConfig config;

    [SerializeField]
    private PlayerInput input;

    [SerializeField]
    private List<BasePlayerFeature> playerFeatures;

    private void Awake()
    {
        
    }

    private void Start()
    {
        InitializeFeatures();
    }

    private void Update()
    {
        UpdateFeatures();
    }

    private void UpdateFeatures()
    {
        playerFeatures.ForEach(x => x.Update());
    }
    
    private void InitializeFeatures()
    {
        playerFeatures.ForEach(x => x.InitializeWithPlayer(this));
    }
}
