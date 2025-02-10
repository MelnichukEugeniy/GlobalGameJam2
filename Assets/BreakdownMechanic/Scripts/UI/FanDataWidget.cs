using UnityEngine;
using UnityEngine.UI;

public class FanDataWidget : Widget
{
    [SerializeField]
    private Button button;
    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private Widget widget;
    
    protected override void Awake()
    {
        base.Awake();
        
        button.onClick.AddListener(OnButtonClicked); // підписка
    }

    private void OnButtonClicked()
    {
        panel.gameObject.SetActive(true);
        
        widget.Show();
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(OnButtonClicked); // відписка 
    }
}