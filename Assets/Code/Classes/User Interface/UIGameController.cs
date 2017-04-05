using UnityEngine;
using UnityEngine.UI;

public class UIGameController : MonoBehaviour
{
    [SerializeField] private Text _ScoreLabel = null;

    public void UpdateScoreLabel (int score)
    {
        _ScoreLabel.text = score.ToString();
    }
}
