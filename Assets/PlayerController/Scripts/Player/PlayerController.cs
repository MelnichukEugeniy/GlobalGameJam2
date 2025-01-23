using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    private PlayerConfig config;

    [SerializeField]
    private PlayerInput input;

    [SerializeField]
    private List<BasePlayerFeature> playerFeatures;

    private Dictionary<Type, BasePlayerFeature> playerFeaturesMap = new Dictionary<Type, BasePlayerFeature>();

    private void Awake()
    {
        LoadFeaturesMap();
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

    private void LoadFeaturesMap()
    {
        foreach (var feature in playerFeatures)
        {
            Type type = feature.GetType();

            playerFeaturesMap[type] = feature;

            Debug.Log(type.FullName);
        }
    }
    
    private void InitializeFeatures()
    {
        playerFeatures.ForEach(x => x.InitializeWithPlayer(this));
    }

    public TFeature GetFeature<TFeature>() where TFeature : BasePlayerFeature
    {
        var featureType = typeof(TFeature);

        if (playerFeaturesMap.TryGetValue(featureType, out var feature))
        {
            return (TFeature) feature;
        }
        
        throw new Exception($"{typeof(TFeature).FullName} feature is not present in player features list");
    }
}
