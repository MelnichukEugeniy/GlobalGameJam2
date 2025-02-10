using UnityEngine;
using UnityEngine.UI;

public class WidgetCheckBox : Widget
{
    [SerializeField]
    private Toggle checkBox; // CheckBox (Toggle)

    [SerializeField]
    private Image confirmationImage; // �������� ������������ (On)

    [SerializeField]
    private Image rejectionImage; // �������� ����������� (Off)

    [SerializeField]
    private bool correctChoice = false; // ���������� ���� CheckBox

    protected override void Awake()
    {
        base.Awake();

        if (checkBox == null || confirmationImage == null || rejectionImage == null)
        {
            Debug.LogError($"[{name}] �� �� ��'���� ���������� � ���������!");
            return;
        }

        // �������� ���� ����� CheckBox
        checkBox.onValueChanged.AddListener(SetWidgetVisibility);

        // ���������� ���� Widget �� ���������
        SetWidgetVisibility(checkBox.isOn);
    }

    private void SetWidgetVisibility(bool isOn)
    {
        if (content == null)
        {
            Debug.LogError($"[{name}] Content �� �����������!");
            return;
        }

        content.SetActive(isOn);

        if (isOn)
        {
            // ���� ������� �������, ������������ ����������� ������
            bool isCorrect = checkBox.isOn == correctChoice;
            confirmationImage.gameObject.SetActive(isCorrect);
            rejectionImage.gameObject.SetActive(!isCorrect);
        }
        else
        {
            // ���� ������� ���������, ��������� ����� ��������
            confirmationImage.gameObject.SetActive(false);
            rejectionImage.gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        if (checkBox != null)
        {
            checkBox.onValueChanged.RemoveListener(SetWidgetVisibility);
        }
    }
}
