using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMeGameManager : MonoBehaviour
{
    public static ColorMeGameManager instance;

    public GameState state;

    public int timer;
    public Difficulty difficulty;
    public String playerName;
    public int playerScore;

    public static event Action<GameState> onGameStateChanged;
    // maby onBeforeGameStateChanged and onAfterGameStateChanged
    
    void Awake() 
    {
        instance = this;
    }

    void Start()
    {
        UpdateGameState(GameState.MENU_WELCOME);
    }
    
    public void UpdateGameState(GameState newState)
    {
        // if (state == newState) return;
        // onBeforeStateChanged?.Invoke(newState);

        state = newState;

        switch (newState)
        {
            case GameState.MENU_WELCOME:
                HandleWelcomeMenu();
                break;
            case GameState.MENU_ONBOARDING:
                HandleOnboardingMenu();
                break;
            case GameState.GAME_PLAY:
                HandleGamePlay();
                break;
            case GameState.MENU_RESULT:
                HandleResultMenu();
                break;
            case GameState.MENU_HIGHSCORE:
                HandleHighscoreMenu();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        // onAfterStateChanged?.Invoke(newState);

        onGameStateChanged?.Invoke(newState);

        Debug.Log($"New state: {newState}.");
    }

    private void HandleWelcomeMenu()
    {
        Debug.Log("Handle Welcome Menu");

        ColorMeUnitManager.Instance.lockColorPaletteAndPicker();
    }

    private void HandleOnboardingMenu()
    {
        Debug.Log("Handle Onboarding Menu");
    }

    private void HandleResultMenu()
    {
        Debug.Log("Handle Result Menu");
    }

    private void HandleGamePlay()
    {
        Debug.Log("Handle Game Play");

        ColorMeUnitManager.Instance.unlockColorPaletteAndPicker();
    }

    private void HandleHighscoreMenu()
    {
        Debug.Log("Handle Highscore Menu");
    }

}

public enum GameState
{
    MENU_WELCOME,
    MENU_ONBOARDING,

    GAME_PLAY,
    GAME_SLIME_COMING,
    GAME_MIX_COLOR,
    GAME_COLOR_SLIME,

    MENU_RESULT,
    MENU_HIGHSCORE
}

public enum Difficulty
{
    EASY,
    NORMAL,
    HARD
}