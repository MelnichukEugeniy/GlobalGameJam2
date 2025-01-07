using UnityEngine;

namespace StateMachinePattern
{
    public interface IState
    {
        void Tick();
        void OnEnter();
        void OnExit();
        Color GizmoColor();
    }
}