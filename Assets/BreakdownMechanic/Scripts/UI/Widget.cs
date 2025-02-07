using UnityEngine;

public class Widget : MonoBehaviour
{
    public bool IsActive => content.gameObject.activeSelf;
    
    [SerializeField]
    private bool hideOnAwake = false;
    [SerializeField]
    protected RectTransform content;
    
    protected virtual void Awake()
    {
        if (content == null)
            content = (RectTransform) transform;
        
        if (hideOnAwake)
        {
            content.gameObject.SetActive(false);
        }
    }

    public virtual void Show()
    {
        content.gameObject.SetActive(true);
    }
    
    public virtual void Hide()
    {
        content.gameObject.SetActive(false);
    }

    public void FlipFlipVisibility()
    {
        if(content.gameObject.activeSelf)
            Hide();
        else
            Show();
    }
}