using UnityEngine;

[CreateAssetMenu(fileName = nameof(PlayerInput), menuName = "Player/" + nameof(PlayerInput))]
public class PlayerInput : ScriptableObject
{
    [SerializeField]
    private KeyCode gasMaskKeyCode;

    public bool GasMaskDown()
    {
        return Input.GetKeyDown(gasMaskKeyCode);
    }
}