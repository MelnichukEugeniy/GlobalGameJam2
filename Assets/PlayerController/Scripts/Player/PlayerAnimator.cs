using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField]
    private string ForwardVelocityKeyName = "Z";

    [SerializeField]
    private string RightVelocityKeyName = "X";

    [SerializeField]
    private string JumpKeyName = "Jump";

    [SerializeField]
    private string CrouchKeyName = "Crouch";

    [SerializeField]
    private string ProneKeyName = "Prone";

    private int forwardVelocityKey;
    private int rightVelocityKey;
    private int jumpKey;
    private int crouchKey;
    private int proneKey;
    private Animator animator;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        
        forwardVelocityKey = Animator.StringToHash(ForwardVelocityKeyName);
        rightVelocityKey = Animator.StringToHash(RightVelocityKeyName);
        jumpKey = Animator.StringToHash(JumpKeyName);
        crouchKey = Animator.StringToHash(CrouchKeyName);
        proneKey = Animator.StringToHash(ProneKeyName);
    }

    public void SetForwardVelocity(float value)
    {
        animator.SetFloat(forwardVelocityKey, value);
    }

    public void SetRightVelocity(float value)
    {
        animator.SetFloat(rightVelocityKey, value);
    }

    public void SetJump(bool value)
    {
        animator.SetBool(jumpKey, value);
    }

    public void SetCrouch(bool value)
    {
        animator.SetBool(crouchKey, value);
    }

    public void SetProne(bool value)
    {
        animator.SetBool(proneKey, value);
    }
}
