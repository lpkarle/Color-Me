using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorMeMenuManager : MonoBehaviour
{
    public static ColorMeMenuManager Instance;

    [SerializeField]
    private GameObject  _panelWelcome, 
                        _panelTutorial,
                        _panelTimerScore, 
                        _panelResult,
                        _panelHighscore,
                        _panelSpeechBubble;

    [SerializeField]
    private TextMeshProUGUI ScoreText;

    [SerializeField]
    private Transform _locationPanel;

    void Awake()
    {
        Instance = this;

        // subscribe to game manager event
        ColorMeGameManager.onGameStateChanged += gameManagerOnGameStateChanged;
    }

    void OnDestroy() {
        // unsubscribe event to prevent memory leaks
        ColorMeGameManager.onGameStateChanged -= gameManagerOnGameStateChanged;
    }

    void Start()
    {  
        _panelWelcome.transform.position = _locationPanel.position;
        _panelWelcome.transform.rotation = _locationPanel.rotation;
        _panelTimerScore.transform.position = _locationPanel.position;
        _panelTimerScore.transform.rotation = _locationPanel.rotation;
        //_panelTutorial.transform.position = _locationPanel.position;
        //_panelTutorial.transform.rotation = _locationPanel.rotation;
        //_panelResult.transform.position = _locationPanel.position;
        //_panelResult.transform.rotation = _locationPanel.rotation;
        _panelHighscore.transform.position = _locationPanel.position;
        _panelHighscore.transform.rotation = _locationPanel.rotation;
    }

    public void UpdateScore()
    {
        ScoreText.text = $"{ColorMeGameManager.instance.playerScore}";
    }

    private void gameManagerOnGameStateChanged(GameState state)
    {
        _panelWelcome.SetActive(state == GameState.MENU_WELCOME);
        _panelTimerScore.SetActive( state == GameState.GAME_PLAY || 
                                    state == GameState.GAME_SLIME_COMING ||
                                    state == GameState.GAME_MIX_COLOR ||
                                    state == GameState.GAME_COLOR_SLIME);
        // TODO _panelTutorial.SetActive(state == GameState.MENU_RESULT);
        // TODO _panelResult.SetActive(state == GameState.MENU_RESULT);
        _panelHighscore.SetActive(state == GameState.MENU_HIGHSCORE);
    }
}
