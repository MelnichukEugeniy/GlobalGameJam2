using System;
using Player.Input;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class MovementSharedValues
    {
        [HideInInspector]
        public float CurrentSpeed;
        [HideInInspector]
        public float TargetHeight;
        [HideInInspector]
        public Vector3 TargetCenter;
        [HideInInspector]
        public float VerticalVelocity;
        
        [HideInInspector]
        public float OriginalHeight;
        [HideInInspector]
        public Vector3 OriginalCenter;
        
        [HideInInspector]
        public Transform Transform;

        public PlayerMovementConfig Config;
        public PlayerMovementInput Input;

        public Transform HeadTransform;
        public CharacterController CharacterController;
    }
}