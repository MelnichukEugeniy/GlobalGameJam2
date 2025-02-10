using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GasMaskEffectWidget : MonoBehaviour
{
    [SerializeField]
    private Image breathImage;

    [SerializeField]
    private GasMaskConfig gasMaskConfig;

    private bool isBreathing;
    private Color breathImageColor;
    private Coroutine breathingCoroutine;
    private EventBinding<GasMaskEquipChangedEvent> gasMaskEquipChangedEventBinding;

    private void Awake()
    {
        breathImageColor = breathImage.color;
        gasMaskEquipChangedEventBinding = new EventBinding<GasMaskEquipChangedEvent>(OnGasMaskEquipChanged);
        
        EventBus<GasMaskEquipChangedEvent>.Register(gasMaskEquipChangedEventBinding);
    }

    private void OnGasMaskEquipChanged(GasMaskEquipChangedEvent @event)
    {
        if (@event.Visibility)
        {
            isBreathing = true;

            breathingCoroutine = StartCoroutine(GasMaskBreathingRoutine());
        }
        else
        {
            isBreathing = false;

            breathImageColor.a = 0;
            breathImage.color = breathImageColor;
            
            if(breathingCoroutine != null)
                StopCoroutine(breathingCoroutine);
        }
    }

    private IEnumerator GasMaskBreathingRoutine()
    {
        int sign = 1;
        float breathTimer = 0;
        
        while (isBreathing)
        {
            breathTimer += Time.deltaTime * sign;
            
            if (breathTimer > gasMaskConfig.BreathInSeconds)
            {
                sign = -1;
            }
            else if(breathTimer < 0)
            {
                sign = 1;
            }

            breathImageColor.a = sign switch {
                1 => Mathf.Lerp(0f, 1f, Mathf.InverseLerp(0, gasMaskConfig.BreathInSeconds, breathTimer)),
                -1 => Mathf.Lerp(1f, 0f, Mathf.InverseLerp(gasMaskConfig.BreathInSeconds, 0, breathTimer)),
                _ => breathImageColor.a
            };

            breathImage.color = breathImageColor;
            yield return null;
        }
    }

    private void OnDestroy()
    {
        EventBus<GasMaskEquipChangedEvent>.Deregister(gasMaskEquipChangedEventBinding);
    }
}