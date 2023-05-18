using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    private GameObject playerXrRig, speechBubble;

    private Vector3 speechBubbleOffset = new Vector3(-0.8f, 1.7f, -0.6f);

    void Start()
    {
        Debug.Log("Slime");
        
        playerXrRig = GameObject.FindWithTag("Player");
        speechBubble = GameObject.FindWithTag("Panel_Speech_Bubble");

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

        Vector3 direction = speechBubble.transform.position - playerXrRig.transform.position;
        speechBubble.transform.rotation = Quaternion.LookRotation(direction); 
        this.speechBubble.SetActive(true);
    }
}
