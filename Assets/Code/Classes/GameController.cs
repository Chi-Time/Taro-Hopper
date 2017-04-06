using UnityEngine;

public class GameController : MonoBehaviour
{
    public int Score { get { return _Score; } set { ChangeScore(value); } }

    [SerializeField] private int _Score = 0;

    private UIController _UIController = null;

    private void Awake ()
    {
        Initialise ();
    }

    private void Initialise ()
    {
        _UIController = GameObject.Find("UI").GetComponent<UIController> ();

        EventManager.OnStateChanged += OnStateSwitched;
    }

    private void Start ()
    {
        SetDefaults ();
    }

    private void SetDefaults ()
    {
    }

    //void OnGUI ()
    //{
    //    GUI.Label (new Rect (100, 150, 450, 450), "SCORE: " + _Score.ToString ());
    //}

    private void ChangeScore (int score)
    {
        _Score = score;

        _UIController.GameScreen.UpdateScoreLabel (_Score);
    }

    private void OnStateSwitched (GameState state)
    {
        switch (state)
        {
            case GameState.Game:
                Time.timeScale = 1.0f;
                break;
            case GameState.Menu:
            case GameState.Pause:
            case GameState.GameOver:
                Time.timeScale = 0.0f;
                break;
            case GameState.Restart:
                RestartGame ();
                break;
        }
    }

    private void RestartGame ()
    {
        //TODO: Add in a way to reset all objects for game restart.
    }

    private void OnDestroy ()
    {
        EventManager.OnStateChanged -= OnStateSwitched;
    }
}
