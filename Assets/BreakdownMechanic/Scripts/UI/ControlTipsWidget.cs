using UnityEngine;

public class ControlTipsWidget : Widget
{
    [SerializeField]
    private GameObject hide;

    [SerializeField]
    private GameObject show;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            FlipFlop();
        }
    }

    private bool showed;
    
    private void FlipFlop()
    {
        if (showed)
        {
            show.SetActive(false);
            hide.SetActive(true);
            showed = false;
            return;
        }
        
        show.SetActive(true);
        hide.SetActive(false);
        showed = true;
    }
}