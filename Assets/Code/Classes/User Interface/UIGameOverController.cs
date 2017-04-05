using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIGameOverController : MonoBehaviour
{
    [SerializeField] private Text _EndScoreLabel = null;

    public void UpdateFinalScoreLabel (int score)
    {
        _EndScoreLabel.text = "Final score is: " + score.ToString ();
    }

    public void RestartGame ()
    {
        //TODO: switch game states.
        SceneManager.LoadScene (0);
    }

    public void ReturnToMenu ()
    {
        //TODO: switch game states.
        EventManager.ChangeState (GameState.Menu);
    }
}
