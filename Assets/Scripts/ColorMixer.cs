using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMixer : MonoBehaviour
{
    [SerializeField]
    private GameObject[] colorObjects;

    [SerializeField]
    private GameObject resultObject;

    private Renderer[] colorObjectsRenderer;

    private Material newMaterial;
    private Color currentColor;
    
    // Start is called before the first frame update
    void Start()
    {
        newMaterial = new Material(Shader.Find("Standard"));
        currentColor = new Color(0.0f, 0.0f, 0.0f);
        newMaterial.color = currentColor;

        Renderer renderer = resultObject.GetComponent<Renderer>();
        renderer.material = newMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("RED");

            if (currentColor.r < 1.0f)
                currentColor.r += 0.1f;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("GREEN");

            if (currentColor.g < 1.0f)           
                currentColor.g += 0.1f;
        }

        if (Input.GetKeyDown(KeyCode.B))
        { 
            Debug.Log("BLUE");

            if (currentColor.b < 1.0f)
                currentColor.b += 0.1f;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("WhITE");
            if (currentColor.r > 0.1f)
                currentColor.r -= 0.1f;

            if (currentColor.g > 0.1f)         
                currentColor.g -= 0.1f;
                        
            if (currentColor.b > 0.1f)
                currentColor.b -= 0.1f;
        }

        newMaterial.color = currentColor;

    }
}
