using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class UIScoreTracker : MonoBehaviour
{
    public IntVariable player1Score;
    public IntVariable player2Score;

    private Label player1ScoreLabel;
    private Label player2ScoreLabel;

    void Start()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        player1ScoreLabel = root.Query<Label>("score_p1");
        player2ScoreLabel = root.Query<Label>("score_p2");

        player1Score.Changed.Register(OnPlayer1ScoreChanged);
        player2Score.Changed.Register(OnPlayer2ScoreChanged);
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
