using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilterBarWidget : MonoBehaviour
{
    [SerializeField]
    private Image fillImage;

    [SerializeField]
    private List<float> filterStages;

    private EventBinding<FilterCapacityLeftChangedEvent> filterCapacityLeftChangedEventBinding;
    
    private void Awake()
    {
        filterCapacityLeftChangedEventBinding = new EventBinding<FilterCapacityLeftChangedEvent>(OnPlayerHealthChanged);
        EventBus<FilterCapacityLeftChangedEvent>.Register(filterCapacityLeftChangedEventBinding);
    }

    private void OnPlayerHealthChanged(FilterCapacityLeftChangedEvent @event)
    {
        float basePercentage = 1f / filterStages.Count;
        float percentage = basePercentage;
        for (int i = 0; i < filterStages.Count; i++)
        {
            if (@event.Feature.FilterPercentage + .001f <= percentage)
            {
                fillImage.fillAmount = filterStages[i]; 
                return;
            }

            percentage += basePercentage;
        }
        
        fillImage.fillAmount = 1; 
    }

    private void OnDestroy()
    {
        EventBus<FilterCapacityLeftChangedEvent>.Deregister(filterCapacityLeftChangedEventBinding);
    }
}
