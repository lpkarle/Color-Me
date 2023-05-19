using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slime : MonoBehaviour
{
    [SerializeField]
    private GameObject WantedColorPrefab;

    private GameObject slimeBody, playerXrRig, speechBubble, WantedColor;

    private Vector3 speechBubbleOffset = new Vector3(-0.8f, 1.7f, -0.6f);

    void Start()
    {
        Debug.Log("Start Slime -----------------------------------");
        
        slimeBody = GameObject.FindWithTag("Slime_Body");
        playerXrRig = GameObject.FindWithTag("Player");
        speechBubble = GameObject.FindWithTag("Panel_Speech_Bubble");

        WantedColor = Instantiate(WantedColorPrefab);
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

        WantedColor.GetComponent<Renderer>().material.color = ColorMeGameManager.instance.currentWantedColor;
        WantedColor.transform.position = speechBubble.transform.position + new Vector3(0.00f, 0.055f, 0.05f);
        WantedColor.transform.rotation = speechBubble.transform.rotation;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (ColorMeGameManager.instance.state != GameState.GAME_MIX_COLOR)
            return;
    
        if (collision.gameObject.CompareTag("Projectile"))
        {
            var newSlimeColor = ColorMeGameManager.instance.currentColorShoot;

            slimeBody.GetComponent<Renderer>().material.color = newSlimeColor;

            Destroy(GameObject.FindWithTag("Projectile"));
            
            ColorMeGameManager.instance.UpdateGameState(GameState.GAME_COLOR_SLIME);

            Destroy(WantedColor);

        }
    }
}
