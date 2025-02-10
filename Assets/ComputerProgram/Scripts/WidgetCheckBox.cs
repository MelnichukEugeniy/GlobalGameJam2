using UnityEngine;
using UnityEngine.UI;

public class WidgetCheckBox : Widget
{
    [SerializeField]
    private Toggle checkBox; // CheckBox (Toggle)

    [SerializeField]
    private Image confirmationImage; // Картинка підтвердження (On)

    [SerializeField]
    private Image rejectionImage; // Картинка заперечення (Off)

    [SerializeField]
    private bool correctChoice = false; // Правильний вибір CheckBox

    protected override void Awake()
    {
        base.Awake();

        if (checkBox == null || confirmationImage == null || rejectionImage == null)
        {
            Debug.LogError($"[{name}] Не всі об'єкти призначено в інспекторі!");
            return;
        }

        // Обробник зміни стану CheckBox
        checkBox.onValueChanged.AddListener(SetWidgetVisibility);

        // Початковий стан Widget та зображень
        SetWidgetVisibility(checkBox.isOn);
    }

    private void SetWidgetVisibility(bool isOn)
    {
        if (content == null)
        {
            Debug.LogError($"[{name}] Content не призначений!");
            return;
        }

        content.SetActive(isOn);

        if (isOn)
        {
            // Якщо чекбокс обраний, перевіряється правильність вибору
            bool isCorrect = checkBox.isOn == correctChoice;
            confirmationImage.gameObject.SetActive(isCorrect);
            rejectionImage.gameObject.SetActive(!isCorrect);
        }
        else
        {
            // Якщо чекбокс вимкнений, приховуємо обидві картинки
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
