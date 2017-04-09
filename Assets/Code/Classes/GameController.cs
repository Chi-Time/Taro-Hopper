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
        EventManager.ChangeState (GameState.Menu);
    }

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
                ResetGame ();
                break;
            case GameState.Pause:
                Time.timeScale = 0.0f;
                break;
            case GameState.GameOver:
                Time.timeScale = 0.0f;
                _UIController.GameOverScreen.UpdateFinalScoreLabel (_Score);
                break;
            case GameState.Restart:
                ResetGame ();
                RestartGame ();
                break;
        }
    }

    private void ResetGame ()
    {
        Time.timeScale = 0.0f;
        GameObject.FindGameObjectWithTag ("Generator").GetComponent<PadGenerator> ().Restart ();
        GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().Kill ();
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
