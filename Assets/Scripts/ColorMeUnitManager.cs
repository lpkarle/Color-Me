using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMeUnitManager : MonoBehaviour
{
    public static ColorMeUnitManager Instance;

    [SerializeField]
    private GameObject colorPalette, colorPicker;

    [SerializeField]
    private GameObject[] slimeGameObjects;

    [SerializeField]
    private Vector3 spawnPosition = new Vector3(2.5f, 2.175f, -2);

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

    public void SpawnSlime()
    {
        var slimeInstance = Instantiate(this.slimeGameObjects[0]);
        slimeInstance.transform.position = this.spawnPosition;
        slimeInstance.SetActive(true);
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
}
