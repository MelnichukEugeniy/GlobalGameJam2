using UnityEngine;

[CreateAssetMenu(fileName = nameof(PlayerMovementConfig), menuName = "Player/MovementConfig")]
public class PlayerMovementConfig : ScriptableObject
{
    public float Speed = 6.0f; // Ўвидк≥сть ходьби
    public float RunSpeed = 12.0f; // Ўвидк≥сть б≥гу
    public float JumpForce = 8.0f; // —ила стрибка
    public float CrouchSpeed = 3.0f; // Ўвидк≥сть прис≥данн€
    public float ProneSpeed = 1.5f; // Ўвидк≥сть л€ганн€
    public float TransitionSpeed = 5.0f; // Ўвидк≥сть плавного переходу м≥ж станами
    
    public float CrouchHeight = 1.2f; // ¬исота персонажа при прис≥данн≥
    public float ProneHeight = 0.8f; // ¬исота персонажа при л€ганн≥

    public Vector3 CrouchCenter = new Vector3(0, 0.1f, 0); // ÷ентр при прис≥данн≥
    public Vector3 ProneCenter = new Vector3(0, 0.7f, 0); // ÷ентр при л€ганн≥

    public LayerMask GroundMask;
}