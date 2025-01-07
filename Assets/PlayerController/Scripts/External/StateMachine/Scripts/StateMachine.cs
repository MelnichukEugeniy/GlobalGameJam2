using System;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachinePattern
{
    public class StateMachine
    {
        private IState _currentState;

        private Dictionary<Type, List<Transition>> _transitions = new();
        private List<Transition> _currentTransitions = new();
        private List<Transition> _anyTransition = new();

        private static List<Transition> _emptyTransitions = new();

        private class Transition
        {
            public Func<bool> Condition { get; }
            public IState To { get; }

            public Transition(IState to, Func<bool> condition)
            {
                To = to;
                Condition = condition;
            }
        }

        public void Tick()
        {
            var transition = GetTransition();
            if (transition != null)
                SetState(transition.To);
            
            _currentState?.Tick();
        }

        public void SetState(IState state)
        {
            if(state == _currentState)
                return;
            
            _currentState?.OnExit();
            _currentState = state;

            _transitions.TryGetValue(_currentState.GetType(), out _currentTransitions);
            if (_currentTransitions == null)
                _currentTransitions = _emptyTransitions;
            
            _currentState.OnEnter();
        }

        public void AddTransition(IState from, IState to, Func<bool> predicate)
        {
            if (_transitions.TryGetValue(from.GetType(), out var targetTransitions) == false)
            {
                targetTransitions = new List<Transition>();
                _transitions[from.GetType()] = targetTransitions;
            }
            
            targetTransitions.Add(new Transition(to, predicate));
        }

        public void AddAnyTransition(IState to, Func<bool> predicate)
        {
            _anyTransition.Add(new Transition(to, predicate));
        }

        public Color GetGizmoColor()
        {
            if (_currentState != null)
                return _currentState.GizmoColor();
            
            return Color.gray;
        }

        private Transition GetTransition()
        {
            foreach (Transition transition in _anyTransition)
            {
                if (transition.Condition())
                {
                    return transition;
                }
            }
            
            foreach (Transition transition in _currentTransitions)
            {
                if (transition.Condition())
                {
                    return transition;
                }
            }
            return null;
        }
    }
}