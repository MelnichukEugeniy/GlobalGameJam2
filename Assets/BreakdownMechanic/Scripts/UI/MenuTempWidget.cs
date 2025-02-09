using UnityEngine;
using UnityEngine.UI;

public class MenuTempWidget : Widget
{
    [SerializeField]
    private Widget infoWidget;
    [SerializeField]
    private Widget ventsWidget;
    [SerializeField]
    private Widget diagnosticWidget;

    [SerializeField]
    private Button infoButton;
    [SerializeField]
    private Button ventsButton;
    [SerializeField]
    private Button diagnosticButton;

    private Widget currentWidget;
    
    protected override void Awake()
    {
        base.Awake();
        
        infoButton.onClick.AddListener(() => FlipFlopWidget(infoWidget));
        ventsButton.onClick.AddListener(() => FlipFlopWidget(ventsWidget));
        diagnosticButton.onClick.AddListener(() => FlipFlopWidget(diagnosticWidget));
    }

    private void FlipFlopWidget(Widget widget)
    {
        if (currentWidget == null)
        {
            currentWidget = widget;
            widget.Show();
            return;
        }
        
        if (currentWidget == widget)
        {
            widget.Hide();
            currentWidget = null;
            return;
        }

        if (currentWidget != widget)
        {
            currentWidget.Hide();
            currentWidget = widget;
            widget.Show();
        }
    }
}