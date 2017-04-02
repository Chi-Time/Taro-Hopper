using UnityEngine;

public class UIController : MonoBehaviour
{
    public UIMenuController MenuScreen = null;
    public UIGameController GameScreen = null;
    public UIPauseController PauseScreen = null;
    public UIGameOverController GameOverScreen = null;

    private void Awake ()
    {
        Intialise ();
    }

    private void Intialise ()
    {
        EventManager.OnStateChanged += OnStateSwitched;
    }

    private void Start ()
    {
        SetDefaults ();
    }

    private void SetDefaults ()
    {
    }

    private void OnStateSwitched (GameState state)
    {
        switch(state)
        {
        case GameState.Menu:
            SwitchScreen (MenuScreen.gameObject);
            break;
        case GameState.Game:
            SwitchScreen (GameScreen.gameObject);
            break;
        case GameState.Pause:
            SwitchScreen (PauseScreen.gameObject);
            break;
        case GameState.GameOver:
            SwitchScreen (GameOverScreen.gameObject);
            break;
        }
    }

    private void SwitchScreen (GameObject screen)
    {
        MenuScreen.gameObject.SetActive (false);
        GameScreen.gameObject.SetActive (false);
        PauseScreen.gameObject.SetActive (false);
        GameOverScreen.gameObject.SetActive (false);

        screen.SetActive (true);
    }

    private void OnDestroy ()
    {
        EventManager.OnStateChanged -= OnStateSwitched;
    }
}
