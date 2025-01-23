using UnityEngine;

public class GasMaskFeature : BasePlayerFeature
{
    [SerializeField]
    private PlayerInput playerInput;

    public bool Visibility { get; private set; }
    
    public override void Update()
    {
        if (playerInput.GasMaskDown())
        {
            FlipFlopVisibility();
        }
    }

    private void FlipFlopVisibility()
    {
        Visibility = !Visibility;
        EventBus<GasMaskFlipFlopEvent>.Raise(new GasMaskFlipFlopEvent(Visibility));
    }
}

public struct GasMaskFlipFlopEvent : IEvent
{
    public bool Visibility;

    public GasMaskFlipFlopEvent(bool visibility)
    {
        Visibility = visibility;
    }
}