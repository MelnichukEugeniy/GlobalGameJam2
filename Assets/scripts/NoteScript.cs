using UnityEngine;

public class NoteScript : MonoBehaviour
{
    private bool _noteStatus;
    public GameObject note;

    public void ToggleNote()
    {
        _noteStatus = !_noteStatus;
        note.SetActive(_noteStatus);
    }

    public bool GetNoteStatus() => _noteStatus;
}
