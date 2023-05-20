using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ColorMixer : MonoBehaviour
{
    [SerializeField]
    private GameObject[] colorObjects;

    [SerializeField]
    private GameObject[] objectsToColor;

    private GameObject projectilePrefab;
    private Transform projectileStartPoint;
    private float projectileLaunchSpeed = 70.0f;
    private float projectileTTL = 2.5f;

    private Renderer[] colorObjectsRenderer;

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

    private void Update() {
        
    }

    public void ShootColor()
    {
        // Only shoot once
        if (ColorMeGameManager.instance.state != GameState.GAME_MIX_COLOR)
            return;

        RaycastHit hit;
        
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Slime"))
            {
                Debug.Log("Ray Hit Slime");

                projectilePrefab = objectsToColor[2];
                
                GameObject projectile = Instantiate(projectilePrefab, transform.position + transform.forward * 0.18f, this.transform.rotation);
                Physics.IgnoreCollision(projectile.GetComponent<Collider>(), this.GetComponent<Collider>());
                
                if (projectile.TryGetComponent(out Rigidbody rigidBody))
                {
                    Vector3 force = this.transform.forward * projectileLaunchSpeed;
                    rigidBody.AddForce(force);
                }

                Destroy(projectile, projectileTTL);

                ColorMeGameManager.instance.currentColorShoot = currentColor;
            }
        }
    }

    public void PickColor()
    {
        colorStep = ColorMeGameManager.instance.difficultyColorSteps;

        RaycastHit hit;

        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, Mathf.Infinity)) // TODO distance to color
        {
            if (hit.collider.CompareTag("Palette_Color_1"))
            {
                Debug.Log("COLOR 1");

                if (currentColor.b < colorMaximumValue)
                    currentColor.b += colorStep;
            }

            if (hit.collider.CompareTag("Palette_Color_2"))
            {
                Debug.Log("COLOR 2");

                if (currentColor.g < colorMaximumValue)
                    currentColor.g += colorStep;
            }

            if (hit.collider.CompareTag("Palette_Color_3"))
            {
                Debug.Log("COLOR 3");
                
                if (currentColor.r < colorMaximumValue)
                    currentColor.r += colorStep;
            }

            if (hit.collider.CompareTag("Palette_Color_Reset"))
            {
                Debug.Log("COLOR Reset");

                if (currentColor.r >= colorStep)
                    currentColor.r -= colorStep;

                if (currentColor.g >= colorStep)         
                    currentColor.g -= colorStep;
                            
                if (currentColor.b >= colorStep)
                    currentColor.b -= colorStep;
            }
        }

        newMaterial.color = currentColor;
    }
}
