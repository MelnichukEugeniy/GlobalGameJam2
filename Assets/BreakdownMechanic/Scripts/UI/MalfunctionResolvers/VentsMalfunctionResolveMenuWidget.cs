using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[DefaultExecutionOrder(-500)]
public class VentsMalfunctionResolveMenuWidget : Widget
{
    private List<UnresolvedMalfunction> unresolvedMalfunctions = new List<UnresolvedMalfunction>();

    [SerializeField]
    private InstructionsWidget instructionsWidget;
    
    [SerializeField]
    private List<VentMalfunctionResolveWidget> malfunctionResolvers;

    private List<VentMalfunction> malfunctions = new List<VentMalfunction>();

    private EventBinding<MalfunctionDetectedEvent> malfunctionDetectedEventBinding;

    protected override void Awake()
    {
        base.Awake();

        malfunctionDetectedEventBinding = new EventBinding<MalfunctionDetectedEvent>(OnMalfunctionDetected);
        EventBus<MalfunctionDetectedEvent>.Register(malfunctionDetectedEventBinding);
    }

    public override void Show()
    {
        base.Show();

        foreach (var malfunction in malfunctions)
        {
            var resolver = GetResolverWithType(malfunction.GetType());
            resolver.Show();
            resolver.MalfunctionResolved += OnMalfunctionResolved;
        }
    }

    public override void Hide()
    {
        base.Hide();

        foreach (var malfunction in malfunctions)
        {
            var resolver = GetResolverWithType(malfunction.GetType());
            resolver.Hide();
            resolver.MalfunctionResolved -= OnMalfunctionResolved;
        }
    }

    private void OnMalfunctionDetected(MalfunctionDetectedEvent @event)
    {
        var malfunction = @event.Malfunction as VentMalfunction;
        var resolver = GetResolverWithType(malfunction.GetType());
        if (resolver == null)
        {
            Debug.Log($"Can't find {malfunction.name}");
            return;
        }
        Debug.Log($"Trying to show resolver for {malfunction.name}");
        malfunctions.Add(malfunction);
        if (IsActive)
        {
            resolver.Show();
            resolver.MalfunctionResolved += OnMalfunctionResolved;
        }
    }

    private void OnMalfunctionResolved(VentMalfunctionResolveWidget resolver, bool resolved)
    {
        var malfunction = malfunctions.FirstOrDefault(x => x.GetType() == resolver.MalfunctionType);
        if (resolved)
        {
            instructionsWidget.ShowWithInstruction(resolver.InstructionText);
            instructionsWidget.Hided += OnInstructionHided;

            void OnInstructionHided()
            {
                resolver.Hide();
                instructionsWidget.Hided -= OnInstructionHided;
            }
            malfunction.Solve();
            return;
        }

        var timer = Timer.CreateTimer(10, 1, false);
        unresolvedMalfunctions.Add(new UnresolvedMalfunction()
        {
            Timer = timer,
            Malfunction = malfunction,
            ResolveWidget = resolver
        });
        
        malfunction.Hide();
        timer.OnTimeout += OnTimeout;
        resolver.Hide();
    }

    private void OnTimeout(Timer timer)
    {
        timer.OnTimeout -= OnTimeout;
        var unresolved = unresolvedMalfunctions.FirstOrDefault(x => x.Timer == timer);
        if(unresolved.ResolveWidget == null)
            return;
        
        var malfunction = malfunctions.FirstOrDefault(x => x.GetType() == unresolved.ResolveWidget.MalfunctionType);
        malfunction.Show();
        unresolvedMalfunctions.Remove(unresolved);
    }

    private VentMalfunctionResolveWidget GetResolverWithType(Type type)
    {
        return malfunctionResolvers.FirstOrDefault(x => x.MalfunctionType == type);
    }

    private void OnDestroy()
    {
        EventBus<MalfunctionDetectedEvent>.Deregister(malfunctionDetectedEventBinding);
    }

    private struct UnresolvedMalfunction
    {
        public Timer Timer;
        public VentMalfunction Malfunction;
        public VentMalfunctionResolveWidget ResolveWidget;
    }
}