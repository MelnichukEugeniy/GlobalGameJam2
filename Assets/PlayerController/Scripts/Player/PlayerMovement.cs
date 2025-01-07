using System;
using Player;
using Player.Movement.States;
using StateMachinePattern;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private ProneState _proneState;
    private CrouchState _crouchState;
    private DefaultPoseState _defaultPoseState;
    
    private StateMachine _movementStateMachine;

    [SerializeField] 
    private MovementSharedValues _sharedValues;
    
    private void Awake()
    {
        _sharedValues.Transform = transform;
        _sharedValues.OriginalHeight = _sharedValues.Collider.height;
        _sharedValues.OriginalCenter = _sharedValues.Collider.center;
        _sharedValues.TargetHeight = _sharedValues.OriginalHeight;
        _sharedValues.TargetCenter = _sharedValues.OriginalCenter;
        
        _movementStateMachine = new StateMachine();

        _proneState = new ProneState(_sharedValues);
        _crouchState = new CrouchState(_sharedValues);
        _defaultPoseState = new DefaultPoseState(_sharedValues);
        
        _movementStateMachine.SetState(_defaultPoseState);

        _movementStateMachine.AddAnyTransition(_proneState, () => _sharedValues.Input.IsProne());
        _movementStateMachine.AddAnyTransition(_crouchState, () => _sharedValues.Input.IsCrouch());
        _movementStateMachine.AddAnyTransition(_defaultPoseState, () => !_sharedValues.Input.AskToChangePose());
    }

    private void Update()
    {
        _movementStateMachine.Tick();
    }

    private void OnDrawGizmosSelected()
    {
        if(_sharedValues.Transform == null)
            return;
        var ray = new Ray(_sharedValues.Transform.position - _sharedValues.OriginalCenter, -_sharedValues.Transform.up * _sharedValues.OriginalHeight / 2f);
        Gizmos.color = _movementStateMachine.GetGizmoColor();
        Gizmos.DrawRay(ray);
    }
}
