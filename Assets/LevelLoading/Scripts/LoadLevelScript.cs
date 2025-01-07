using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelScript : MonoBehaviour
{
    public string levelToLoad;
    public TMP_Text percentLoaded;
    private GameObject loadingMessage;
    private AsyncOperation loadingOperation;

    public void LoadLevelAsync()
    {
        loadingMessage.SetActive(true);

        loadingOperation = SceneManager.LoadSceneAsync(levelToLoad);
    }

    public bool GetLoadingStatus() => loadingOperation != null && loadingOperation.isDone;

    private void Start()
    {
        loadingMessage = GameObject.Find("LoadingMessage");
        loadingMessage.SetActive(false);
    }

    void Update()
    {
        if (loadingOperation == null)
            return;

        float progressValue = Mathf.Clamp01(loadingOperation.progress / 0.9f);

        percentLoaded.text = Mathf.Round(progressValue * 100) + "%";
    }
}
