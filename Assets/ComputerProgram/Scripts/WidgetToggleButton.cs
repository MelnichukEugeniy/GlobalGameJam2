using UnityEngine;
using UnityEngine.UI;

public class WidgetToggleButton : Widget
{
    [SerializeField]
    private Button button; // Кнопка для управления видимостью

    protected override void Awake()
    {
        base.Awake();

        if (button == null)
        {
            Debug.LogError($"[{name}] Button не назначен в инспекторе!");
            return;
        }

        button.onClick.AddListener(ToggleVisibility); // Подписка на клик кнопки
    }

    private void ToggleVisibility()
    {
        if (content == null)
        {
            Debug.LogError($"[{name}] Content не назначен!");
            return;
        }

        if (content.activeSelf)
        {
            Hide();
            Debug.Log($"[{name}] Widget скрыт");
        }
        else
        {
            Show();
            Debug.Log($"[{name}] Widget открыт");
        }
    }

    private void OnDestroy()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(ToggleVisibility); // Отписка от события
        }
    }
}
