using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    public bool TimerOn = false;

    private float timeLeft;

    public TextMeshProUGUI TimerTxt;

    void Start()
    {
        TimerOn = true;
        timeLeft = ColorMeGameManager.Instance.Timer;
    }

    void Update()
    {
        if (TimerOn)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                UpdateTimer(timeLeft);
            }
            else
            {
                Debug.Log("Time is UP!");
                timeLeft = 0;
                TimerOn = false;
                ColorMeGameManager.Instance.Timer = timeLeft;
                ColorMeGameManager.Instance.UpdateGameState(GameState.MENU_RESULT);
            }
        }
    }

    private void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
