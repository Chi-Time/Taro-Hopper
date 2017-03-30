using UnityEngine;

public class GameController : MonoBehaviour
{
    public int Score { get { return _Score; } set {ChangeScore(value); } }

    [SerializeField] private int _Score = 0;

    private void Awake ()
    {
        Initialise ();
    }

    private void Initialise ()
    {
    }

    private void Start ()
    {
        SetDefaults ();
    }

    private void SetDefaults ()
    {
    }

    void OnGUI ()
    {
        GUI.Label (new Rect (100, 150, 450, 450), "SCORE: " + _Score.ToString ());
    }

    private void ChangeScore (int score)
    {
        _Score = score;
    }
}