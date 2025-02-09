using System;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] 
    private Camera playerCamera;
    [SerializeField] 
    private LayerMask interactableLayerMask;
    [SerializeField] 
    private float interactRange = 3f;
    [SerializeField] 
    private float lookInteractionDelaySeconds = 0.2f;

    private bool isLookingAtInteractObject;
    private ILookInteractable lookInteractableObject;
    private Timer lookInteractionTimer;

    private void Awake()
    {
        lookInteractionTimer = Timer.CreateTimer(lookInteractionDelaySeconds, 1, true);
        lookInteractionTimer.OnTimeout += OnLookInteractionTimerTimeout;
    }

    private void OnLookInteractionTimerTimeout(Timer _)
    {
        LookInteract();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
    }

    private void LookInteract()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactRange, interactableLayerMask))
        {
            if (hit.collider.TryGetComponent<ILookInteractable>(out var lookInteractable))
            {
                if (isLookingAtInteractObject)
                    return;

                isLookingAtInteractObject = true;
                lookInteractableObject = lookInteractable;
                lookInteractable.OnLookEnter();
            }
        }
        else
        {
            if(!isLookingAtInteractObject)
                return;
            
            isLookingAtInteractObject = false;
            lookInteractableObject?.OnLookExit();
            lookInteractableObject = null;
        }
    }

    private void TryInteract()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactRange))
        {
            if (hit.collider.TryGetComponent<IInteractable>(out var interactable))
            {
                interactable.Interact();
            }
        }
    }

    private void OnDestroy()
    {
        lookInteractionTimer.OnTimeout -= OnLookInteractionTimerTimeout;
    }
}