using UnityEngine;

[CreateAssetMenu(fileName = nameof(PlayerInput), menuName = "Player/" + nameof(PlayerInput))]
public class PlayerInput : ScriptableObject
{
    [SerializeField]
    private KeyCode gasMaskKeyCode;

    [SerializeField]
    private KeyCode equipNewFilterKeyCode;
    
    public bool GasMaskDown()
    {
        return Input.GetKeyDown(gasMaskKeyCode);
    }

    public bool EquipNewFilterDown()
    {
        return Input.GetKeyDown(equipNewFilterKeyCode);
    }
}