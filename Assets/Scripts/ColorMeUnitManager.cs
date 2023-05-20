using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class ColorMeUnitManager : MonoBehaviour
{
    public static ColorMeUnitManager Instance;

    [SerializeField]
    private GameObject colorPalette, colorPicker, smokeVFX;

    [SerializeField]
    private GameObject[] slimeGameObjects;

    [SerializeField]
    private Vector3 spawnPosition = new Vector3(2.5f, 2.175f, -2);

    private GameObject slimeInstance;

    private TouchScreenKeyboard keyboard;

    private void Awake() 
    {
        Instance = this;
    }

    public void StartGameEasy()
    {
        Debug.Log("Start Easy Pressed");
        ColorMeGameManager.instance.difficulty = Difficulty.EASY;
        ColorMeGameManager.instance.difficultyColorSteps = DifficultyColorSteps.EASY;
        
        ColorMeGameManager.instance.UpdateGameState(GameState.GAME_PLAY);
    }

    public void StartGameNormal()
    {
        Debug.Log("Start Normal Pressed");
        ColorMeGameManager.instance.difficulty = Difficulty.NORMAL;
        ColorMeGameManager.instance.difficultyColorSteps = DifficultyColorSteps.NORMAL;

        ColorMeGameManager.instance.UpdateGameState(GameState.GAME_PLAY);
    }

    public void StartGameHard()
    {
        Debug.Log("Start Hard Pressed");
        ColorMeGameManager.instance.difficulty = Difficulty.HARD;
        ColorMeGameManager.instance.difficultyColorSteps = DifficultyColorSteps.HARD;

        ColorMeGameManager.instance.UpdateGameState(GameState.GAME_PLAY);
    }

    public void lockColorPaletteAndPicker()
    {
        Debug.Log("Start Hard Pressed");
        colorPalette.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        colorPicker.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    public void unlockColorPaletteAndPicker()
    {
        colorPalette.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        colorPicker.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }

    public async void SpawnSlime()
    {
        PlaySmokeVFX();

        await Delay(500);

        if (ColorMeGameManager.instance.state != GameState.MENU_RESULT)
        {
            slimeInstance = Instantiate(this.slimeGameObjects[0]);
            slimeInstance.transform.position = this.spawnPosition;
            slimeInstance.SetActive(true);

            ColorMeGameManager.instance.UpdateGameState(GameState.GAME_MIX_COLOR);
        }
    }

    public async void DestroySlime()
    {
        await Delay(1000);

        PlaySmokeVFX();

        slimeInstance.SetActive(false);

        await Delay(1000);

        if (ColorMeGameManager.instance.state != GameState.MENU_RESULT)
            ColorMeGameManager.instance.UpdateGameState(GameState.GAME_SLIME_COMING);
    }

    public void GenerateWantedSlimeColor()
    {
        float colorSteps = ColorMeGameManager.instance.difficultyColorSteps;
        var red = GetRandomFloat(colorSteps);
        var green = GetRandomFloat(colorSteps);
        var blue = GetRandomFloat(colorSteps);

        Debug.Log("Wanted Color: " + red + " " + green + " " + blue);

        ColorMeGameManager.instance.currentWantedColor = new Color(red, green, blue);
    }

    private float GetRandomFloat(float stepSize)
    {
        float numberSteps = 1.0f / stepSize;
        int step = (int) Random.Range(0, numberSteps + 1);

        return step * stepSize;
    }

    public void ReturnToWelcomeScreen()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowHighscore()
    {
        ColorMeGameManager.instance.UpdateGameState(GameState.MENU_HIGHSCORE);
    }

    public void SaveScore()
    {
        var inputPlayerName = GameObject.FindWithTag("Input_Player_Name").GetComponent<TMP_InputField>().text;
        ColorMeGameManager.instance.playerName = inputPlayerName;
        Debug.Log(
            "-------------------- SaveScore"
        );
        ColorMeGameManager.instance.UpdateGameState(GameState.MENU_HIGHSCORE);
    }

    public void ShowKeyboard()
    {
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }

    private void PlaySmokeVFX()
    {
        var smokeInstance = Instantiate(smokeVFX);
        smokeInstance.transform.localScale *= 0.7f;
        smokeInstance.transform.position = this.spawnPosition;
        Destroy(smokeInstance, 3.0f);
    }

    private async Task Delay(int milliseconds)
    {
        Debug.Log("START DELAY");

        await Task.Delay(milliseconds);
    }
}
