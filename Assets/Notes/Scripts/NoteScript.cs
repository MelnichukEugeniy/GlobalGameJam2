using UnityEngine;

public class NoteScript : MonoBehaviour
{
    public GameObject note;
    private bool noteStatus;

    public void ToggleNote()
    {
        noteStatus = !noteStatus;
        note.SetActive(noteStatus);
    }

    public bool GetNoteStatus() => noteStatus;
}
