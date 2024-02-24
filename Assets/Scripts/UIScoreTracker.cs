using UnityEngine;
using UnityEngine.UIElements;

public class UIScoreTracker : MonoBehaviour
{
    // Start is called before the first frame update
    Label player1ScoreLabel;
    Label player2ScoreLabel;

    void Start()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        player1ScoreLabel = root.Query<Label>("score_p1");
        player2ScoreLabel = root.Query<Label>("score_p2");
    }

    public void OnPlayer1ScoreChanged(int value)
    {
        player1ScoreLabel.text = value.ToString();
    }

    public void OnPlayer2ScoreChanged(int value)
    {
        player2ScoreLabel.text = value.ToString();
    }
}
