using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private UIMenuController _MenuScreen = null;
    [SerializeField] private UIGameController _GameScreen = null;
    [SerializeField] private UIPauseController _PauseScreen = null;
    [SerializeField] private UIGameOverController _GameOverScreen = null;

    private void Awake ()
    {
        Intialise ();
    }

    private void Intialise ()
    {
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
            SwitchScreen (_MenuScreen.gameObject);
            break;
        case GameState.Game:
            SwitchScreen (_GameScreen.gameObject);
            break;
        case GameState.Pause:
            SwitchScreen (_PauseScreen.gameObject);
            break;
        case GameState.GameOver:
            SwitchScreen (_GameOverScreen.gameObject);
            break;
        }
    }

    private void SwitchScreen (GameObject screen)
    {
        _MenuScreen.gameObject.SetActive (false);
        _GameScreen.gameObject.SetActive (false);
        _PauseScreen.gameObject.SetActive (false);
        _GameOverScreen.gameObject.SetActive (false);

        screen.SetActive (true);
    }
}
