using UnityEngine;

public delegate void ChangeState (GameState state);

public static class EventManager
{
    public static event ChangeState OnStateChanged;

    public static void ChangeState (GameState state)
    {
        OnStateChanged (state);
        Debug.Log ("Current game state is: " + state.ToString ());
    }
}
