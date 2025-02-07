using UnityEngine;
using UnityEngine.Events;

public class Widget : MonoBehaviour
{
    [SerializeField]
    private bool hideOnAwake = false;

    [SerializeField]
    protected GameObject content;

    // ��䳿 ��� ������� �� ����� �� ������������
    public UnityEvent OnShow;
    public UnityEvent OnHide;

    protected virtual void Awake()
    {
        if (content == null)
        {
            Debug.LogError($"[{name}] content �� ������! ³���� �� ����������� ��������.");
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
        Debug.Log($"[{name}] ³���� ��������.");
        OnShow?.Invoke();
    }

    public virtual void Hide()
    {
        if (content == null) return;

        content.SetActive(false);
        Debug.Log($"[{name}] ³���� ���������.");
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
