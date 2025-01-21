using TMPro;
using UnityEngine;

public class PlayerLoadLevelScript : MonoBehaviour
{
    public TMP_Text interactionMessage;
    private LoadLevelScript loadLevelTrigger;

    // Update is called once per frame
    void Update()
    {
        if (loadLevelTrigger != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                loadLevelTrigger.LoadLevelAsync();
                interactionMessage.enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LoadLevel"))
        {
            other.gameObject.TryGetComponent(out loadLevelTrigger);
            interactionMessage.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("LoadLevel"))
        {
            loadLevelTrigger = null;
            interactionMessage.enabled = false;
        }
    }
}
