using UnityEngine;
using UnityEngine.Events;

public class Widget : MonoBehaviour
{
    [SerializeField]
    private bool hideOnAwake = false;

    [SerializeField]
    protected GameObject content;

    // Події для реакції на показ та приховування
    public UnityEvent OnShow;
    public UnityEvent OnHide;

    protected virtual void Awake()
    {
        if (content == null)
        {
            Debug.LogError($"[{name}] content не задано! Віджет не працюватиме коректно.");
            return;
        }

        if (hideOnAwake)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    public virtual void Show()
    {
        if (content == null) return;

        content.SetActive(true);
        Debug.Log($"[{name}] Віджет показано.");
        OnShow?.Invoke();
    }

    public virtual void Hide()
    {
        if (content == null) return;

        content.SetActive(false);
        Debug.Log($"[{name}] Віджет приховано.");
        OnHide?.Invoke();
    }

    public void ToggleVisibility()
    {
        if (content == null) return;

        if (content.activeSelf)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }
}
