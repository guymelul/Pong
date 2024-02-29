using System;
using UnityAtoms.PongGame;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class UIMainMenu : MonoBehaviour
{
    public GameSessionDataVariable gameSession;
    private Button btnArcade;
    private Button btnVs;

    private bool isBusy = false;

    void Start()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        btnArcade = root.Query<Button>("btn_arcade");
        btnVs = root.Query<Button>("btn_vs");

        btnArcade.clicked += onArcadeClick;
        btnVs.clicked += onVsClick;

        isBusy = false;
    }

    void onArcadeClick()
    {
        ChangeGameSession(true, false);
    }

    void onVsClick()
    {
        ChangeGameSession(true, true);
    }

    private void ChangeGameSession(bool player1IsHuman, bool player2IsHuman)
    {
        if (isBusy) return;
        isBusy = true;

        GameSessionData gameSessionData = (GameSessionData) gameSession.Value.Clone();

        gameSessionData.Player1.IsHuman = player1IsHuman;
        gameSessionData.Player2.IsHuman = player2IsHuman;

        gameSession.SetValue(gameSessionData);

        SwitchToGameSession();
    }

    private void SwitchToGameSession()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
}
