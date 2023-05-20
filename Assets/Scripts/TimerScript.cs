using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerScript : MonoBehaviour
{
    public bool TimerOn = false;
    private float TimeLeft;

    public TextMeshProUGUI TimerTxt;

    void Start()
    {
        TimerOn = true;
        TimeLeft = ColorMeGameManager.instance.Timer;
    }

    void Update()
    {
        if (TimerOn)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);
            }
            else
            {
                Debug.Log("Time is UP!");
                TimeLeft = 0;
                TimerOn = false;
                ColorMeGameManager.instance.Timer = TimeLeft;
                ColorMeGameManager.instance.UpdateGameState(GameState.MENU_RESULT);
            }
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}
