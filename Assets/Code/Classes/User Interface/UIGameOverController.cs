using UnityEngine;

public class UIGameOverController : MonoBehaviour
{
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
