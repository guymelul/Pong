using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace MalulsArcade.Pong
{
    [RequireComponent(typeof(UIDocument))]
    public class UIMainMenu : MonoBehaviour
    {
        public PongSessionData gameSession;
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

            gameSession.Player1.IsHuman = player1IsHuman;
            gameSession.Player2.IsHuman = player2IsHuman;

            SwitchToGameSession();
        }

        private void SwitchToGameSession()
        {
            SceneManager.LoadScene("PongGameScene", LoadSceneMode.Single);
        }
    }
}