using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _MenuScreen = null;
    [SerializeField] private GameObject _GameScreen = null;
    [SerializeField] private GameObject _PauseScreen = null;
    [SerializeField] private GameObject _GameOverScreen = null;

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
            SwitchScreen (_MenuScreen);
            break;
        case GameState.Game:
            SwitchScreen (_GameScreen);
            break;
        case GameState.Pause:
            SwitchScreen (_PauseScreen);
            break;
        case GameState.GameOver:
            SwitchScreen (_GameOverScreen);
            break;
        }
    }

    private void SwitchScreen (GameObject screen)
    {
        _MenuScreen.SetActive (false);
        _GameScreen.SetActive (false);
        _PauseScreen.SetActive (false);
        _GameOverScreen.SetActive (false);

        screen.SetActive (true);
    }
}