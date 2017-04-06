using UnityEngine;
using UnityEngine.UI;

public class UIPauseController : MonoBehaviour
{
    public void MuteMusic (bool isMuted)
    {
        //TODO: Connect with audio player and mute music.

    }

    public void MuteSFX (bool isMuted)
    {
        //TODO: Connect with audio players and mute music.

    }

    public void ReturnToMenu ()
    {
        //TODO: Switch game state.
        EventManager.ChangeState (GameState.Menu);
    }
}
