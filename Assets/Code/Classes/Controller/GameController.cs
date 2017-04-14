using UnityEngine;

public class GameController : MonoBehaviour
{
    public int Score { get { return _Score; } set { ChangeScore(value); } }

    public static GameState CurrentState;

    [SerializeField] private int _Score = 0;

    private UIController _UIController = null;
    private const string _HighScoreKey = "High Score";

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
        EventManager.ChangeState (GameState.Menu);
    }

    private void ChangeScore (int score)
    {
        _Score = score;

        _UIController.GameScreen.UpdateScoreLabel (_Score);
    }

    private void SaveHighScore ()
    {
        if(_Score > PlayerPrefs.GetInt (_HighScoreKey))
            PlayerPrefs.SetInt (_HighScoreKey, _Score);
    }

    private void OnStateSwitched (GameState state)
    {
        CurrentState = state;

        switch (state)
        {
            case GameState.Game:
                Time.timeScale = 1.0f;
                Cursor.visible = false;
                break;
            case GameState.Menu:
                Cursor.visible = true;
                ResetScene ();
                break;
            case GameState.Pause:
                Cursor.visible = true;
                Time.timeScale = 0.0f;
                break;
            case GameState.GameOver:
                Cursor.visible = true;
                Time.timeScale = 0.0f;
                SaveHighScore ();
                _UIController.GameOverScreen.UpdateFinalScoreLabel (_Score, PlayerPrefs.GetInt (_HighScoreKey));
                break;
            case GameState.Restart:
                Cursor.visible = true;
                ResetScene ();
                RestartGame ();
                break;
        }
    }

    private void ResetScene ()
    {
        Time.timeScale = 0.0f;
        GameObject.FindGameObjectWithTag ("Generator").GetComponent<PadGenerator> ().Restart ();
        GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().ResetPlayer ();
        GameObject.FindGameObjectWithTag ("MainCamera").transform.position = new Vector3 (0f, 3f, -10f);
        Score = 0;
    }

    private void RestartGame ()
    {
        EventManager.ChangeState (GameState.Game);
    }

    private void OnDestroy ()
    {
        EventManager.OnStateChanged -= OnStateSwitched;
    }
}
