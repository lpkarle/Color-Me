using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField]
    private GameObject wantedColorPrefab, speechBubble;

    [SerializeField]
    private AudioSource AudioSourceVFX;

    [SerializeField]
    private List<AudioClip> AudioClips;

    private GameObject slimeBody, playerXrRig, wantedColor;

    private Vector3 speechBubbleOffset = new(-0.8f, 1.7f, -0.6f);
    private Vector3 wantedColorOffset = new(0.00f, 0.055f, 0.05f);

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
        wantedColor = Instantiate(wantedColorPrefab);

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

    void SetFace(Texture tex) => faceMaterial.SetTexture("_MainTex", tex);

    private void FaceThePlayer()
    {
        Vector3 direction = playerXrRig.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    private void ShowSpeechBubble()
    {
        speechBubble.transform.position = transform.position + speechBubbleOffset;

        Vector3 direction = speechBubble.transform.position - playerXrRig.transform.position;
        speechBubble.transform.rotation = Quaternion.LookRotation(direction); 

        if (wantedColor != null)
        {
            wantedColor.GetComponent<Renderer>().material.color = ColorMeGameManager.Instance.currentWantedColor;
            wantedColor.transform.SetPositionAndRotation(
                speechBubble.transform.position + wantedColorOffset,
                speechBubble.transform.rotation);
        }
        
        if(ColorMeGameManager.Instance.state == GameState.MENU_RESULT)
        {
            speechBubble.SetActive(false);

            if (wantedColor != null)
                Destroy(wantedColor);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (ColorMeGameManager.Instance.state != GameState.GAME_MIX_COLOR)
            return;

        if (Mathf.Approximately(ColorMeGameManager.Instance.Timer, 0.0f))
            return;

        if (collision.gameObject.CompareTag("Projectile"))
        {
            AudioSourceVFX.PlayOneShot(AudioClips[0]);

            var newSlimeColor = ColorMeGameManager.Instance.currentColorShoot;

            slimeBody.GetComponent<Renderer>().material.color = newSlimeColor;

            Destroy(GameObject.FindWithTag("Projectile"));
            
            ColorMeGameManager.Instance.UpdateGameState(GameState.GAME_COLOR_SLIME);

            Destroy(wantedColor);
            speechBubble.SetActive(false);
        }
    }

    private void ShowFaceByState()
    {
        switch (ColorMeGameManager.Instance.CurrentSlimeFace)
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