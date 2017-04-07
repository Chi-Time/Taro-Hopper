using UnityEngine;

public class UIController : MonoBehaviour
{
    [HideInInspector] public UIMenuController MenuScreen = null;
    [HideInInspector] public UIGameController GameScreen = null;
    [HideInInspector] public UIPauseController PauseScreen = null;
    [HideInInspector] public UIGameOverController GameOverScreen = null;

    private void Awake ()
    {
        Intialise ();
    }

    private void Intialise ()
    {
        MenuScreen = GameObjectUtilities.GetChildComponent<UIMenuController> (this.transform);
        GameScreen = GameObjectUtilities.GetChildComponent<UIGameController> (this.transform);
        PauseScreen = GameObjectUtilities.GetChildComponent<UIPauseController> (this.transform);
        GameOverScreen = GameObjectUtilities.GetChildComponent<UIGameOverController> (this.transform);

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
        switch (state)
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
