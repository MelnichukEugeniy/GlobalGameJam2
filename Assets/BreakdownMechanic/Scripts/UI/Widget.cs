using System;
using UnityEngine;

public class Widget : MonoBehaviour
{
    [SerializeField]
    private bool hideOnAwake = false;

    protected virtual void Awake()
    {
        if (hideOnAwake)
        {
            gameObject.SetActive(false);
        }
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }
    
    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}