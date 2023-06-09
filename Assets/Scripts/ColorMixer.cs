using System.Collections.Generic;
using UnityEngine;

public class ColorMixer : MonoBehaviour
{
    [SerializeField]
    private GameObject[] colorObjects;

    [SerializeField]
    private GameObject[] objectsToColor;

    [SerializeField]
    private AudioSource AudioSource;

    [SerializeField]
    private List<AudioClip> AudioClips;

    private GameObject projectilePrefab;
    private float projectileLaunchSpeed = 70.0f;
    private float projectileTTL = 2.5f;

    private float colorStep;

    private Material newMaterial;
    private Color currentColor;
    private float colorMaximumValue = 1.0f;

    void Start()
    {
        Debug.Log("COLOR MIXER colorStep: " + colorStep);

        // Set all objects to color to black => inital state
        newMaterial = new Material(Shader.Find("Standard"));
        currentColor = new Color(0.0f, 0.0f, 0.0f);
        newMaterial.color = currentColor;

        foreach (var objToColor in objectsToColor)
        {   
            Renderer renderer = objToColor.GetComponent<Renderer>();
            renderer.material = newMaterial;
        }
    }

    public void ShootColor()
    {
        // Only shoot once
        if (ColorMeGameManager.Instance.state != GameState.GAME_MIX_COLOR)
            return;

        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Slime"))
            {
                Debug.Log("Ray Hit Slime");

                projectilePrefab = objectsToColor[2];
                
                GameObject projectile = Instantiate(projectilePrefab, transform.position + transform.forward * 0.18f, transform.rotation);
                Physics.IgnoreCollision(projectile.GetComponent<Collider>(), this.GetComponent<Collider>());
                
                if (projectile.TryGetComponent(out Rigidbody rigidBody))
                {
                    Vector3 force = transform.forward * projectileLaunchSpeed;
                    rigidBody.AddForce(force);

                    AudioSource.volume = 0.4f; 
                    AudioSource.PlayOneShot(AudioClips[1]);
                }

                Destroy(projectile, projectileTTL);

                ColorMeGameManager.Instance.currentColorShoot = currentColor;
            }
        }
    }

    public void PickColor()
    {
        colorStep = ColorMeGameManager.Instance.difficultyColorSteps;

        RaycastHit hit;

        var distanceToColor = 0.1f;

        if (Physics.Raycast(transform.position, transform.forward, out hit, distanceToColor))
        {
            AudioSource.volume = 0.8f; 

            if (hit.collider.CompareTag("Palette_Color_1"))
            {
                AudioSource.PlayOneShot(AudioClips[0]);

                if (currentColor.b < colorMaximumValue)
                    currentColor.b += colorStep;
            }

            if (hit.collider.CompareTag("Palette_Color_2"))
            {
                AudioSource.PlayOneShot(AudioClips[0]);

                if (currentColor.g < colorMaximumValue)
                    currentColor.g += colorStep;
            }

            if (hit.collider.CompareTag("Palette_Color_3"))
            {
                AudioSource.PlayOneShot(AudioClips[0]);
                
                if (currentColor.r < colorMaximumValue)
                    currentColor.r += colorStep;
            }

            if (hit.collider.CompareTag("Palette_Color_Reset"))
            {
                AudioSource.PlayOneShot(AudioClips[0]);

                if (currentColor.r >= colorStep)
                    currentColor.r -= colorStep;

                if (currentColor.g >= colorStep)         
                    currentColor.g -= colorStep;
                            
                if (currentColor.b >= colorStep)
                    currentColor.b -= colorStep;
            }

            ColorMeGameManager.Instance.currentColorShoot = currentColor;
        }

        newMaterial.color = currentColor;
    }
}
