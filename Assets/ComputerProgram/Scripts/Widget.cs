using UnityEngine;

public class Widget : MonoBehaviour
{
    public bool IsActive => content.gameObject.activeSelf;
    
    [SerializeField]
    private bool hideOnAwake = false;

    [SerializeField]
    protected GameObject content;

    protected virtual void Awake()
    {
        if(content == null)
            content = transform.gameObject;
        
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
        content.SetActive(true);
    }

    public virtual void Hide()
    {
        content.SetActive(false);
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
