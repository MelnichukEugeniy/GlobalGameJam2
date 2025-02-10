using UnityEngine;

public class ComputerInterface : MonoBehaviour, IInteractable
{
    [SerializeField]
    private ComputerWidget computerWidget;
    
    public void Interact()
    {
        computerWidget.Show();
    }
}
