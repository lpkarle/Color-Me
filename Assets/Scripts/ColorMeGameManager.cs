using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMeGameManager : MonoBehaviour
{
    public static ColorMeGameManager instance;

    public GameState state;

    public Difficulty difficulty;
    public float difficultyColorSteps;

    public Color currentWantedColor;
    public Color currentColorShoot;

    public float Timer;
    public String playerName;
    public int playerScore;
    public SlimeFaceState CurrentSlimeFace;

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
        /* if (state == newState) return;
            onBeforeStateChanged?.Invoke(newState); */

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
            case GameState.GAME_SLIME_COMING:
                HandleSlimeComing();
                break;
            case GameState.GAME_MIX_COLOR:
                HandleMixColor();
                break;
            case GameState.GAME_COLOR_SLIME:
                HandleColorSlime();
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

        Timer = 30.0f;
        playerName = "-";
        playerScore = 0;

        ColorMeUnitManager.Instance.lockColorPaletteAndPicker();
    }

    private void HandleOnboardingMenu()
    {
        Debug.Log("Handle Onboarding Menu");
    }

    private void HandleResultMenu()
    {
        Debug.Log("Handle Result Menu");
        ColorMeUnitManager.Instance.DestroySlime();
    }

    private void HandleGamePlay()
    {
        Debug.Log("Handle Game Play");

        ColorMeUnitManager.Instance.unlockColorPaletteAndPicker();
        
        UpdateGameState(GameState.GAME_SLIME_COMING);
    }

    private void HandleSlimeComing()
    {
        Debug.Log("Handle Slime Coming");

        CurrentSlimeFace = SlimeFaceState.IDLE;
        ColorMeUnitManager.Instance.SpawnSlime();
        ColorMeUnitManager.Instance.GenerateWantedSlimeColor();
    }

    private void HandleMixColor()
    {
        Debug.Log("Handle Mix Color");
    }

    private void HandleColorSlime()
    {
        Debug.Log("Handle Color Slime");

        CalculatePointsByMixedColor();
        ColorMeMenuManager.Instance.UpdateScore();

        // TODO Animation Happy/Sad
        // TODO Animation Despawn

        ColorMeUnitManager.Instance.DestroySlime();
    }

    private void HandleHighscoreMenu()
    {
        Debug.Log("Handle Highscore Menu");
    }

    private void CalculatePointsByMixedColor()
    {
        var currentWantedColorVector = new Vector3(currentWantedColor.r, currentWantedColor.g, currentWantedColor.b);
        var currentColorShootVector = new Vector3(currentColorShoot.r, currentColorShoot.g, currentColorShoot.b);
        float colorDistanceAbs = Mathf.Abs(Vector3.Distance(currentWantedColorVector, currentColorShootVector));

        // TODO schöner machen, Map!!!!!!!!!!!!!!!!!!!!!!!
        float distanceExact = 0.0f;
        float distanceInexact = 0.0f;
        float distanceIninExact = 0.0f;
        int pointsExact = 0;
        int pointsInexact = 0;
        int pointsIninExact = 0;

        switch (difficulty)
        {
            case Difficulty.EASY:
                distanceExact = 0.0f;
                distanceInexact = 0.5f;
                distanceIninExact = 1.0f;
                pointsExact = 10;
                pointsInexact = 5;
                pointsIninExact = 0;
                break;
            case Difficulty.NORMAL:
                distanceExact = 0.0f;
                distanceInexact = 0.25f;
                distanceIninExact = 0.5f;
                pointsExact = 50;
                pointsInexact = 25;
                pointsIninExact = 10;
                break;
            case Difficulty.HARD:
                distanceExact = 0.0f;
                distanceInexact = 0.2f;
                distanceIninExact = 0.5f;
                pointsExact = 2000;
                pointsInexact = 50;
                pointsIninExact = 10;
                break;
        }
        // TODO schöner machen, Map!!!!!!!!!!!!!!!!!!!!!!!

        if (Mathf.Approximately(colorDistanceAbs, distanceExact))
        {
            playerScore += pointsExact;
            CurrentSlimeFace = SlimeFaceState.HAPPY;
        }
        else if (colorDistanceAbs <= distanceInexact)
        {
            playerScore += pointsInexact;
            CurrentSlimeFace = SlimeFaceState.HAPPY;
        }
        else if (colorDistanceAbs <= distanceIninExact)
        {
            playerScore += pointsIninExact;
            CurrentSlimeFace = SlimeFaceState.SAD;
        }
        else
            CurrentSlimeFace = SlimeFaceState.DEAD;
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

public static class DifficultyColorSteps 
{
    public const float EASY = 0.5f;
    public const float NORMAL = 0.25f;
    public const float HARD = 0.1f;
}