using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slime : MonoBehaviour
{
    [SerializeField]
    private GameObject WantedColorPrefab, speechBubble;

    [SerializeField]
    private AudioSource AudioSourceVFX;

    [SerializeField]
    private List<AudioClip> AudioClips;

    private GameObject slimeBody, playerXrRig, WantedColor;

    private Vector3 speechBubbleOffset = new Vector3(-0.8f, 1.7f, -0.6f);

    public Face faces;
    public Animator animator;
    private Material faceMaterial;

    public bool startWalking = false;

    void Start()
    {
        Debug.Log("Start Slime -----------------------------------");
        
        slimeBody = GameObject.FindWithTag("Slime_Body");
        playerXrRig = GameObject.FindWithTag("Player");
        
        speechBubble.SetActive(true);
        WantedColor = Instantiate(WantedColorPrefab);

        faceMaterial = slimeBody.GetComponent<Renderer>().materials[1];
        SetFace(faces.Idleface);
    }

    void Update()
    {
        ShowFaceByState();

        if (!startWalking)
            FaceThePlayer();
            
        ShowSpeechBubble();
    }

    void SetFace(Texture tex)
    {
        faceMaterial.SetTexture("_MainTex", tex);
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

        if (WantedColor != null)
        {
            WantedColor.GetComponent<Renderer>().material.color = ColorMeGameManager.instance.currentWantedColor;
            WantedColor.transform.position = speechBubble.transform.position + new Vector3(0.00f, 0.055f, 0.05f);
            WantedColor.transform.rotation = speechBubble.transform.rotation;
        }
        
        if(ColorMeGameManager.instance.state == GameState.MENU_RESULT)
        {
            speechBubble.SetActive(false);

            if (WantedColor != null)
                Destroy(WantedColor);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (ColorMeGameManager.instance.state != GameState.GAME_MIX_COLOR)
            return;

        if (Mathf.Approximately(ColorMeGameManager.instance.Timer, 0.0f))
            return;

        if (collision.gameObject.CompareTag("Projectile"))
        {
            AudioSourceVFX.PlayOneShot(AudioClips[0]);

            var newSlimeColor = ColorMeGameManager.instance.currentColorShoot;

            slimeBody.GetComponent<Renderer>().material.color = newSlimeColor;

            Destroy(GameObject.FindWithTag("Projectile"));
            
            ColorMeGameManager.instance.UpdateGameState(GameState.GAME_COLOR_SLIME);

            Destroy(WantedColor);
            speechBubble.SetActive(false);
        }
    }

    private void ShowFaceByState()
    {
        switch (ColorMeGameManager.instance.CurrentSlimeFace)
        {
            case SlimeFaceState.IDLE:
                SetFace(faces.Idleface);
                break;
            case SlimeFaceState.HAPPY:
                SetFace(faces.jumpFace);
                break;
            case SlimeFaceState.SAD:
                SetFace(faces.attackFace);
                break;
            case SlimeFaceState.DEAD:
                SetFace(faces.damageFace);
                break;
        }
    }

}

public enum SlimeFaceState 
{ 
    IDLE, 
    HAPPY, 
    SAD, 
    DEAD 
}