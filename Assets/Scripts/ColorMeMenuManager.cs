using UnityEngine;
using TMPro;

public class ColorMeMenuManager : MonoBehaviour
{
    public static ColorMeMenuManager Instance;

    [SerializeField]
    private GameObject  panelWelcome, 
                        panelTimerScore, 
                        panelResult,
                        panelHighscore;

    [SerializeField]
    private TextMeshProUGUI TimerScoreText, ResultScoreText;

    [SerializeField]
    private GameObject NonNativeKeyboard;

    [SerializeField]
    private Transform _locationPanel;

    private Vector3 keyboardOffset = new(0.0f, 0.2f, 0.0f);

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
        panelWelcome.transform.SetPositionAndRotation(_locationPanel.position, _locationPanel.rotation);
        panelTimerScore.transform.SetPositionAndRotation(_locationPanel.position, _locationPanel.rotation);
        panelResult.transform.SetPositionAndRotation(_locationPanel.position, _locationPanel.rotation);
        panelHighscore.transform.SetPositionAndRotation(_locationPanel.position, _locationPanel.rotation);
        NonNativeKeyboard.transform.SetPositionAndRotation(_locationPanel.position + keyboardOffset, _locationPanel.rotation);
    }

    public void UpdateScore()
    {
        TimerScoreText.text = $"{ColorMeGameManager.Instance.playerScore}";
        ResultScoreText.text = $"{ColorMeGameManager.Instance.playerScore}";
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
