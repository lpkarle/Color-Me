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

    private Renderer[] colorObjectsRenderer;

    private Material newMaterial;
    private Color currentColor;
    private float colorStep;
    private float colorMaximumValue = 1.0f;

    void Start()
    {
        switch (ColorMeGameManager.instance.difficulty)
        {
            case Difficulty.EASY:
                colorStep = 0.5f;
                break;
            case Difficulty.MEDIUM:
                colorStep = 0.25f;
                break;
            case Difficulty.HARD:
                colorStep = 0.1f;
                break;
        }

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

    // Update is called once per frame
    void Update()
    {
    }

    public void ShootColor()
    {
    }

    public void PickColor()
    {
        RaycastHit hit;
        //bool isHit = Physics.Raycast(this.transform.position, this.transform.forward, out hit);
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, Mathf.Infinity))
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
