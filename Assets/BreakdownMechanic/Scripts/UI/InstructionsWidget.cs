using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsWidget : Widget
{
    public event Action Hided; 

    [SerializeField]
    private TMP_Text instructionTextField;
    
    [SerializeField]
    private Button closeButton;
    
    protected override void Awake()
    {
        base.Awake();
        closeButton.onClick.AddListener(Hide);
    }

    public override void Hide()
    {
        base.Hide();
        Hided?.Invoke();
    }

    public void ShowWithInstruction(string instruction)
    {
        instructionTextField.text = instruction;
        Show();
    }
}