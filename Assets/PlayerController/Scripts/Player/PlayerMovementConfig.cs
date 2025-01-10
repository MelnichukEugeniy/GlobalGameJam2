using UnityEngine;

[CreateAssetMenu(fileName = nameof(PlayerMovementConfig), menuName = "Player/MovementConfig")]
public class PlayerMovementConfig : ScriptableObject
{
    public float Speed = 6.0f; // �������� ������
    public float RunSpeed = 12.0f; // �������� ���
    public float JumpForce = 8.0f; // ���� �������
    public float CrouchSpeed = 3.0f; // �������� ���������
    public float ProneSpeed = 1.5f; // �������� �������
    public float TransitionSpeed = 5.0f; // �������� �������� �������� �� �������
    
    public float CrouchHeight = 1.2f; // ������ ��������� ��� ��������
    public float ProneHeight = 0.8f; // ������ ��������� ��� ������

    public Vector3 CrouchCenter = new Vector3(0, 0.1f, 0); // ����� ��� ��������
    public Vector3 ProneCenter = new Vector3(0, 0.7f, 0); // ����� ��� ������

    public LayerMask GroundMask;
}