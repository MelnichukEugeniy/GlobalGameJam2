using UnityEngine;
using UnityEngine.UI;

public class ComputerWidget : Widget
{
    [SerializeField]
    private Button hideButton;

    protected override void Awake()
    {
        base.Awake();
        hideButton.onClick.AddListener(Hide);
    }


    public override void Show()
    {
        base.Show();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public override void Hide()
    {
        base.Hide();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}