using System.Collections;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIMatchState : MonoBehaviour
{
    private AudioSource audioSource;

    public KeyCode startMatchKeyCode = KeyCode.Space;
    private Label gameStateLabel;
    private Button backButton;

    public VoidEvent matchStarted;

    private bool acceptsInput;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        gameStateLabel = root.Query<Label>("game_state");
        gameStateLabel.text = "";

        backButton = root.Query<Button>("btn_back");

        backButton.clicked += onBackButtonClicked;

        DisableInput();
    }

    private void DisableInput()
    {
        acceptsInput = false;
    }

    private void EnableInput()
    {
        acceptsInput = true;
    }

    private void onBackButtonClicked()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void onMatchPrepared()
    {
        EnableInput();
        gameStateLabel.text = string.Format("Press {0} to start", startMatchKeyCode.ToString());
        backButton.style.display = DisplayStyle.None;
    }

    public void onMatchEnded(int player)
    {
        gameStateLabel.text = string.Format("Player {0} WON!", player);
        backButton.style.display = DisplayStyle.Flex;
    }

    private IEnumerator onStartMatch()
    {
        DisableInput();

        if (audioSource != null)
        {
            audioSource.PlayOneShot(audioSource.clip);

            for (int i = (int) audioSource.clip.length; i> 0; i--)
            {
                gameStateLabel.text = i.ToString();
                yield return new WaitForSeconds(1);
            }

            gameStateLabel.text = "";
        }

        matchStarted.Raise();
    }

    private void Update()
    {
        if (!acceptsInput)
        {
            return;
        }

        if (Input.GetKeyUp(startMatchKeyCode))
        {
            StartCoroutine(onStartMatch());
        }
    }
}
