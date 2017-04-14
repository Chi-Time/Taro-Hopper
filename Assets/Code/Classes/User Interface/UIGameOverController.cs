using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIGameOverController : MonoBehaviour
{
    [SerializeField] private Text _EndScoreLabel = null;
    [SerializeField] private Text _HighScoreLabel = null;

    public void UpdateFinalScoreLabel (int score, int highScore)
    {
        _EndScoreLabel.text = "Final score is: " + score.ToString ();
        _HighScoreLabel.text = "High score is: " + highScore.ToString ();
    }

    public void RestartGame ()
    {
        //TODO: switch game states.
        EventManager.ChangeState (GameState.Restart);
    }

    public void ReturnToMenu ()
    {
        //TODO: switch game states.
        EventManager.ChangeState (GameState.Menu);
    }
}
