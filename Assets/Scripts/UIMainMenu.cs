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
    private Button btnExit;

    private bool isBusy = false;

    void Start()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        btnArcade = root.Query<Button>("btn_arcade");
        btnVs = root.Query<Button>("btn_vs");
        btnExit = root.Query<Button>("btn_exit");

        btnArcade.clicked += onArcadeClick;
        btnVs.clicked += onVsClick;
        btnExit.clicked += onExitClick;

        isBusy = false;
    }

    private void onArcadeClick()
    {
        ChangeGameSession(true, false);
    }

    private void onVsClick()
    {
        ChangeGameSession(true, true);
    }

    private void onExitClick()
    {
        Application.Quit();
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
