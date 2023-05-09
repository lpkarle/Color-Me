using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Slime : MonoBehaviour
{
    private GameObject playerXrRig, speechBubble;

    private TextMeshProUGUI speechBubbleText;
    private Vector3 speechBubbleOffset = new Vector3(0, 2, 0);

    void Start()
    {
        Debug.Log("Slime");
        
        playerXrRig = GameObject.FindWithTag("Player");
        speechBubble = GameObject.FindWithTag("Panel_Speech_Bubble");
        speechBubbleText = speechBubble.GetComponentInChildren<TextMeshProUGUI>();

        Debug.Log(speechBubble.transform.position);
    }

    void Update()
    {
        FaceThePlayer();

        ShowSpeechBubble();
    }

    private void FaceThePlayer()
    {
        Vector3 direction = playerXrRig.transform.position - this.transform.position;
        this.transform.rotation = Quaternion.LookRotation(direction);
    }

    private void ShowSpeechBubble()
    {
        speechBubble.transform.position = this.transform.position + speechBubbleOffset;

        speechBubbleText.text = "Mach mich blau!";

        Vector3 direction = speechBubble.transform.position - playerXrRig.transform.position;
        speechBubble.transform.rotation = Quaternion.LookRotation(direction); 
        this.speechBubble.SetActive(true);
    }
}
