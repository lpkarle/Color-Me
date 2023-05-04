using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorMeMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _panelWelcome, _panelPause, _panelResult;

    [SerializeField]
    private Button _btnStart;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void gameManagerOnGameStateChanged(GameState state)
    {
        _panelWelcome.SetActive(state == GameState.MENU_WELCOME);

        _btnStart.interactable = state == GameState.MENU_WELCOME;
    }

    public void BtnStartPressed()
    {
        Debug.Log("Start Pressed");

        ColorMeGameManager.instance.UpdateGameState(GameState.GAME_INIT);

    }
}
