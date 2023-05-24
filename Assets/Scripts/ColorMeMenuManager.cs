using UnityEngine;
using TMPro;

public class ColorMeMenuManager : MonoBehaviour
{
    public static ColorMeMenuManager Instance;

    [SerializeField]
    private GameObject panelWelcome, 
                       panelTimerScore, 
                       panelResult,
                       panelHighscore;

    [SerializeField]
    private TextMeshProUGUI timerScoreText, resultScoreText;

    [SerializeField]
    private GameObject nonNativeKeyboard;

    [SerializeField]
    private Transform locationPanel;

    private Vector3 keyboardOffset = new(0.0f, 0.32f, 0.0f);
    private Vector3 keyboardRotation = new(-20f, 0f, 0f);

    void Awake()
    {
        Instance = this;

        ColorMeGameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    void OnDestroy()
    {
        ColorMeGameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    void Start()
    {  
        panelWelcome.transform.SetPositionAndRotation(locationPanel.position, locationPanel.rotation);
        panelTimerScore.transform.SetPositionAndRotation(locationPanel.position, locationPanel.rotation);
        panelResult.transform.SetPositionAndRotation(locationPanel.position, locationPanel.rotation);
        panelHighscore.transform.SetPositionAndRotation(locationPanel.position, locationPanel.rotation);
        nonNativeKeyboard.transform.SetPositionAndRotation(locationPanel.position + keyboardOffset, locationPanel.rotation);
        nonNativeKeyboard.transform.Rotate(keyboardRotation);
    }

    public void UpdateScore()
    {
        timerScoreText.text = $"{ColorMeGameManager.Instance.playerScore}";
        resultScoreText.text = $"{ColorMeGameManager.Instance.playerScore}";
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        panelWelcome.SetActive(state == GameState.MENU_WELCOME);
        panelTimerScore.SetActive( state == GameState.GAME_PLAY || 
                                    state == GameState.GAME_SLIME_COMING ||
                                    state == GameState.GAME_MIX_COLOR ||
                                    state == GameState.GAME_COLOR_SLIME);
        panelResult.SetActive(state == GameState.MENU_RESULT);
        panelHighscore.SetActive(state == GameState.MENU_HIGHSCORE);
    }
}
