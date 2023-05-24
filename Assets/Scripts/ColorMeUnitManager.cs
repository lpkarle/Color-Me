using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ColorMeUnitManager : MonoBehaviour
{
    public static ColorMeUnitManager Instance;

    [SerializeField]
    private GameObject colorPalette, colorPicker, smokeVFX;

    [SerializeField]
    private GameObject[] slimeGameObjects;

    [SerializeField]
    private AudioSource AudioSourceVFX, AudioSourceSlime;

    [SerializeField]
    private List<AudioClip> AudioClips;

    [SerializeField]
    private Vector3 spawnPosition = new Vector3(2.5f, 2.175f, -2);

    private GameObject slimeInstance;

    private TouchScreenKeyboard keyboard;

    private void Awake() 
    {
        Instance = this;
    }

    public async void StartGameEasy()
    {
        Debug.Log("Start Easy Pressed");
        ColorMeGameManager.Instance.difficulty = Difficulty.EASY;
        ColorMeGameManager.Instance.difficultyColorSteps = DifficultyColorSteps.EASY;

        await Delay(500);
        
        ColorMeGameManager.Instance.UpdateGameState(GameState.GAME_PLAY);
    }

    public async void StartGameNormal()
    {
        Debug.Log("Start Normal Pressed");
        ColorMeGameManager.Instance.difficulty = Difficulty.NORMAL;
        ColorMeGameManager.Instance.difficultyColorSteps = DifficultyColorSteps.NORMAL;

        await Delay(500);

        ColorMeGameManager.Instance.UpdateGameState(GameState.GAME_PLAY);
    }

    public async void StartGameHard()
    {
        Debug.Log("Start Hard Pressed");
        ColorMeGameManager.Instance.difficulty = Difficulty.HARD;
        ColorMeGameManager.Instance.difficultyColorSteps = DifficultyColorSteps.HARD;
        
        await Delay(500);

        ColorMeGameManager.Instance.UpdateGameState(GameState.GAME_PLAY);
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

        if (ColorMeGameManager.Instance.state != GameState.MENU_RESULT)
        {
            slimeInstance = Instantiate(this.slimeGameObjects[0]);
            slimeInstance.transform.position = this.spawnPosition;
            slimeInstance.SetActive(true);

            ColorMeGameManager.Instance.UpdateGameState(GameState.GAME_MIX_COLOR);
        }
    }

    public async void DestroySlime()
    {
        await Delay(500); // show face

        var slimeInstanceScript = slimeInstance.GetComponent<Slime>();
        slimeInstanceScript.startWalking = true;

        await Delay(2500);

        slimeInstance.SetActive(false);

        if (ColorMeGameManager.Instance.state != GameState.MENU_RESULT)
            ColorMeGameManager.Instance.UpdateGameState(GameState.GAME_SLIME_COMING);
    }

    public void GenerateWantedSlimeColor()
    {
        float colorSteps = ColorMeGameManager.Instance.difficultyColorSteps;
        var red = GetRandomFloat(colorSteps);
        var green = GetRandomFloat(colorSteps);
        var blue = GetRandomFloat(colorSteps);

        Debug.Log("Wanted Color: " + red + " " + green + " " + blue);

        ColorMeGameManager.Instance.currentWantedColor = new Color(red, green, blue);
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
        ColorMeGameManager.Instance.UpdateGameState(GameState.MENU_HIGHSCORE);
    }

    public void SaveScore()
    {
        var inputPlayerName = GameObject.FindWithTag("Input_Player_Name").GetComponent<TMP_InputField>().text;
        ColorMeGameManager.Instance.playerName = inputPlayerName;
        Debug.Log(
            "-------------------- SaveScore"
        );
        ColorMeGameManager.Instance.UpdateGameState(GameState.MENU_HIGHSCORE);
    }

    public void ShowKeyboard()
    {
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }

    public void PlaySlimeColoredSound()
    {
        switch (ColorMeGameManager.Instance.CurrentSlimeFace)
        {
            case SlimeFaceState.HAPPY:
                AudioSourceVFX.PlayOneShot(AudioClips[2]);
                break;
            case SlimeFaceState.IDLE:
                AudioSourceVFX.PlayOneShot(AudioClips[3]);
                break;
            case SlimeFaceState.SAD:
                AudioSourceVFX.PlayOneShot(AudioClips[4]);
                break;
            case SlimeFaceState.DEAD:
                AudioSourceVFX.PlayOneShot(AudioClips[5]);
                break;
        }
    }

    public void PlayResultSound()
    {
        AudioSourceVFX.PlayOneShot(AudioClips[1]);
    }

    private void PlaySmokeVFX()
    {
        AudioSourceVFX.PlayOneShot(AudioClips[0]);

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
