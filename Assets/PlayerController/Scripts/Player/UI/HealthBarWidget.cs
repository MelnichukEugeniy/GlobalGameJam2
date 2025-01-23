using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarWidget : MonoBehaviour
{
    [SerializeField]
    private Image fillImage;

    [SerializeField]
    private List<float> healthStages;

    private EventBinding<PlayerHealthChangedEvent> playerHealthChangedEventBinding;
    
    private void Awake()
    {
        playerHealthChangedEventBinding = new EventBinding<PlayerHealthChangedEvent>(OnPlayerHealthChanged);
        EventBus<PlayerHealthChangedEvent>.Register(playerHealthChangedEventBinding);
    }

    private void OnPlayerHealthChanged(PlayerHealthChangedEvent @event)
    {
        float basePercentage = 1f / healthStages.Count;
        float percentage = basePercentage;
        for (int i = 0; i < healthStages.Count; i++)
        {
            Debug.Log($"Percentage: {percentage}");
            if (@event.Feature.HealthPercentage + .01f <= percentage)
            {
                fillImage.fillAmount = healthStages[i]; 
                return;
            }

            percentage += basePercentage;
        }
    }

    private void OnDestroy()
    {
        EventBus<PlayerHealthChangedEvent>.Deregister(playerHealthChangedEventBinding);
    }
}
