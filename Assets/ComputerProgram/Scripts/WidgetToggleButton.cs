using UnityEngine;
using UnityEngine.UI;

public class WidgetToggleButton : Widget
{
    [SerializeField]
    private Button button; // ������ ��� ���������� ����������

    protected override void Awake()
    {
        base.Awake();

        if (button == null)
        {
            Debug.LogError($"[{name}] Button �� �������� � ����������!");
            return;
        }

        button.onClick.AddListener(ToggleVisibility); // �������� �� ���� ������
    }

    private void ToggleVisibility()
    {
        if (content == null)
        {
            Debug.LogError($"[{name}] Content �� ��������!");
            return;
        }

        if (content.activeSelf)
        {
            Hide();
            Debug.Log($"[{name}] Widget �����");
        }
        else
        {
            Show();
            Debug.Log($"[{name}] Widget ������");
        }
    }

    private void OnDestroy()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(ToggleVisibility); // ������� �� �������
        }
    }
}
