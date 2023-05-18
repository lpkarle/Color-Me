using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorMeMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject  _panelWelcome, 
                        _panelTutorial,
                        _panelTimerScore, 
                        _panelResult,
                        _panelHighscore,
                        _panelSpeechBubble;

    [SerializeField]
    private Transform _locationPanel;

    [SerializeField]
    private Button  _btnStartEasy,
                    _btnStartNormal,
                    _btnStartHard,
                    _btnStartTutorial,
                    _btnStartHighscore,
                    _btnHighscoreBackToStart;
                    
    void Awake()
    {
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

    private void gameManagerOnGameStateChanged(GameState state)
    {
        _panelWelcome.SetActive(state == GameState.MENU_WELCOME);
        _panelTimerScore.SetActive(state == GameState.GAME_PLAY);
        // TODO _panelTutorial.SetActive(state == GameState.MENU_RESULT);
        // TODO _panelResult.SetActive(state == GameState.MENU_RESULT);
        _panelHighscore.SetActive(state == GameState.MENU_HIGHSCORE);

        // _btnStart.interactable = state == GameState.MENU_WELCOME;
    }

    public void BtnStartPressed()
    {
        Debug.Log("Start Pressed");
    }
}
