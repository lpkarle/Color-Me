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


    // Start is called before the first frame update
    void Start()
    { }

    

    public void StartGameEasy()
    {
        Debug.Log("Start Easy Pressed");
        ColorMeGameManager.instance.difficulty = Difficulty.EASY;
        ColorMeGameManager.instance.UpdateGameState(GameState.GAME_PLAY);
    }

    public void StartGameNormal()
    {
        Debug.Log("Start Normal Pressed");
        ColorMeGameManager.instance.difficulty = Difficulty.NORMAL;
        ColorMeGameManager.instance.UpdateGameState(GameState.GAME_PLAY);
    }

    public void StartGameHard()
    {
        Debug.Log("Start Hard Pressed");
        ColorMeGameManager.instance.difficulty = Difficulty.HARD;
        ColorMeGameManager.instance.UpdateGameState(GameState.GAME_PLAY);
    }

    /* public void InitGame()
    {
        var slimeInstance = Instantiate(this.slimeGameObjects[0]);
        slimeInstance.transform.position = this.spawnPosition;
        slimeInstance.SetActive(true);
    } */


    public void lockColorPaletteAndPicker()
    {
        colorPalette.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        colorPicker.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    public void unlockColorPaletteAndPicker()
    {
        colorPalette.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        colorPicker.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }
}
