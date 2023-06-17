using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] private bool mainMenu;
    public Animator fadeAnimator;
    public Animator gamemodeAnimator;
    public Animator creditsAnimator;
    private GamemodeCanvas gamemodeCanvas;
    private CreditsCanvas creditsCanvas;
    private int levelToLoad;

    private void Start()
    {
        if(mainMenu)
        {
            gamemodeCanvas = FindObjectOfType<GamemodeCanvas>();
            gamemodeCanvas.gameObject.SetActive(false);
            creditsCanvas = FindObjectOfType<CreditsCanvas>();
            creditsCanvas.gameObject.SetActive(false);

        }
        
    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        fadeAnimator.SetTrigger("fadeOut");
    }

    public void FadeToQuit()
    {
        fadeAnimator.SetTrigger("fadeOut");
        Application.Quit();
    }

    public void OpenGamemodeMenu()
    {
        gamemodeCanvas.gameObject.SetActive(true);
    }

    public void CloseGamemodeMenu()
    {
        gamemodeAnimator.SetTrigger("closeGamemode");
    }

    public void OpenCredits()
    {
        creditsCanvas.gameObject.SetActive(true);
    }

    public void CloseCredits()
    {
        creditsAnimator.SetTrigger("closeCredits");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void OnMoveComplete()
    {
        gamemodeCanvas.gameObject.SetActive(false);
    }
}
