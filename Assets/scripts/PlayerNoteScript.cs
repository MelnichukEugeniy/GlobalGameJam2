using UnityEngine;

public class PlayerNoteScript : MonoBehaviour
{
    private NoteScript activeNote;
    private GameObject interactionMessage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactionMessage = GameObject.Find("InteractionMessage");
        interactionMessage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (activeNote != null)
        {
            if (Input.GetKeyDown("e"))
            {
                activeNote.ToggleNote();
                interactionMessage?.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Note")
        {
            other.gameObject.TryGetComponent(out activeNote);
            interactionMessage?.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Note")
        {
            if (activeNote.GetNoteStatus())
                activeNote.ToggleNote();

            activeNote = null;
            interactionMessage?.SetActive(false);
        }
    }
}
