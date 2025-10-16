using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] private string gameScene = "Game";


    [SerializeField] private CanvasGroup canvasGroup; 
    [SerializeField] private float fadeDuration = 1.5f;


    private void Awake()
    {
        if (!canvasGroup)
            canvasGroup = GetComponent<CanvasGroup>();

        canvasGroup.alpha = 0f;     
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
    private void Start()
    {
        FadeIn();
    }

    public void FadeIn()
    {
        StartCoroutine(FadeCoroutine(0f, 1f));
    }

    private IEnumerator FadeCoroutine(float from, float to)
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = to;

        if (to > from)
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
    }
    public void FadeOutAndLoadScene(string sceneName)
    {
        StartCoroutine(FadeOutCoroutine(sceneName));
    }

    private IEnumerator FadeOutCoroutine(string sceneName)
    {
        yield return FadeCoroutine(1f, 0f);
        SceneManager.LoadScene(sceneName);
    }

    public void StartGame()
    {
        FadeOutAndLoadScene(gameScene);
    }
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
