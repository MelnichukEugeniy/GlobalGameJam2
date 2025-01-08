using System;
using Player;
using Player.Movement.States;
using StateMachinePattern;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private ProneState proneState;
    private CrouchState crouchState;
    private DefaultPoseState defaultPoseState;
    
    private StateMachine movementStateMachine;

    [SerializeField] 
    private MovementSharedValues sharedValues;
    
    private void Awake()
    {
        sharedValues.Transform = transform;
        sharedValues.OriginalHeight = sharedValues.Collider.height;
        sharedValues.OriginalCenter = sharedValues.Collider.center;
        sharedValues.TargetHeight = sharedValues.OriginalHeight;
        sharedValues.TargetCenter = sharedValues.OriginalCenter;
        
        movementStateMachine = new StateMachine();

        proneState = new ProneState(sharedValues);
        crouchState = new CrouchState(sharedValues);
        defaultPoseState = new DefaultPoseState(sharedValues);
        
        movementStateMachine.SetState(defaultPoseState);

        movementStateMachine.AddAnyTransition(proneState, () => sharedValues.Input.IsProne());
        movementStateMachine.AddAnyTransition(crouchState, () => sharedValues.Input.IsCrouch());
        movementStateMachine.AddAnyTransition(defaultPoseState, () => !sharedValues.Input.AskToChangePose());
    }

    private void Update()
    {
        movementStateMachine.Tick();
    }

    private void OnDrawGizmosSelected()
    {
        if(sharedValues.Transform == null)
            return;
        var ray = new Ray(sharedValues.Transform.position - sharedValues.OriginalCenter, -sharedValues.Transform.up * sharedValues.OriginalHeight / 2f);
        Gizmos.color = movementStateMachine.GetGizmoColor();
        Gizmos.DrawRay(ray);
    }
}
