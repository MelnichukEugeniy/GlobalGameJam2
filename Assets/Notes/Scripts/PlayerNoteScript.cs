using TMPro;
using UnityEngine;

public class PlayerNoteScript : MonoBehaviour
{
    public TMP_Text interactionMessage;
    private NoteScript activeNote;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactionMessage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (activeNote != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                activeNote.ToggleNote();
                interactionMessage.enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Note"))
        {
            other.gameObject.TryGetComponent(out activeNote);
            interactionMessage.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Note"))
        {
            if (activeNote.GetNoteStatus())
                activeNote.ToggleNote();

            activeNote = null;
            interactionMessage.enabled = false;
        }
    }
}
