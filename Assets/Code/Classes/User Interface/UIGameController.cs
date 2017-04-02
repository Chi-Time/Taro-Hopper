using UnityEngine;
using UnityEngine.UI;

public class UIGameController : MonoBehaviour
{
    private Text _CurrentScore = null;

    public void UpdateScore (int score)
    {
        _CurrentScore.text = score.ToString();
    }
}