using UnityEngine;

public class PlayerLoadLevelScript : MonoBehaviour
{
    private LoadLevelScript loadLevelTrigger;
    private GameObject interactionMessage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactionMessage = GameObject.Find("InteractionMessage");
    }

    // Update is called once per frame
    void Update()
    {
        if (loadLevelTrigger != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                loadLevelTrigger.LoadLevelAsync();
                interactionMessage.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LoadLevel"))
        {
            other.gameObject.TryGetComponent(out loadLevelTrigger);
            interactionMessage.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("LoadLevel"))
        {
            loadLevelTrigger = null;
            interactionMessage.SetActive(false);
        }
    }
}
