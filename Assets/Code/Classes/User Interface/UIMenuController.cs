using UnityEngine;

public class UIMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _MenuPanel = null;
    [SerializeField] private GameObject _CreditsPanel = null;

    private bool _IsInCredit = false;

    public void StartGame ()
    {
        //TODO: Add in state switch to game scene.
        EventManager.ChangeState (GameState.Game);
    }

    public void Credits ()
    {
        _IsInCredit = !_IsInCredit;

        if(!_IsInCredit)
        {
            _MenuPanel.SetActive(false);
            _CreditsPanel.SetActive(true);
        }
        else
        {
            _MenuPanel.SetActive(true);
            _CreditsPanel.SetActive(false);
        }
    }

    public void EndGame ()
    {
        Application.Quit ();
    }

    public void MuteMusic ()
    {
        //TODO: Connect with audio player and mute music.

    }

    public void MuteSFX ()
    {
        //TODO: Connect with audio players and mute music.

    }
}
