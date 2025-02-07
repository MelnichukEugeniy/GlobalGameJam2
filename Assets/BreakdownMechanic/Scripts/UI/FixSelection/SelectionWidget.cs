using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionWidget : Widget
{
    [SerializeField]
    private Color activeColor;
    [SerializeField]
    private Color disActiveColor;
    
    [SerializeField]
    private Image backgroundImage;
    [SerializeField]
    private TMP_Text textField;


    public void Activate()
    {
        backgroundImage.color = activeColor;
    }
    
    public void Disactivate()
    {
        backgroundImage.color = disActiveColor;
    }

    public void WithText(string text)
    {
        textField.text = text;
    }
}