using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class MainMenuWidget : MonoBehaviour
{
    [SerializeField]
    private KeyCode openCloseMenuKeyCode = KeyCode.Escape;

    [SerializeField]
    private VideoPlayer videoPlayer;
    
    [SerializeField]
    private RectTransform panelTransform;
    
    [SerializeField]
    private Button playButton;

    [SerializeField]
    private Button exitButton;

    private AudioSource videoAudioSource;
    
    private void Awake()
    {
        playButton.onClick.AddListener(OnPlayPressed);
        exitButton.onClick.AddListener(OnExitPressed);

        if (SceneManager.GetActiveScene().name != "SampleScene")
        {
            Hide();
            return;
        }
        
        Show();
    }

    private void Update()
    {
        if (Input.GetKeyDown(openCloseMenuKeyCode))
        {
            if (panelTransform.gameObject.activeSelf)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }
    }

    public void Show()
    {
        Debug.Log("Show");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        videoPlayer.SetDirectAudioMute(0, false);
        panelTransform.gameObject.SetActive(true);
    }

    private void Hide()
    {
        Debug.Log("Hide");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        videoPlayer.SetDirectAudioMute(0, true);
        panelTransform.gameObject.SetActive(false);
    }

    private void OnPlayPressed()
    {
        Hide();
        //SceneManager.LoadScene("SampleScene");
    }

    private void OnExitPressed()
    {
        Application.Quit();
    }
}
