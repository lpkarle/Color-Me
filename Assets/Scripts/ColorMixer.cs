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

    /* [SerializeField] private ActionBasedController actionBasedController;*/

    [SerializeField] private GameObject slime; 


    /* private void OnEnable()
    {
        actionBasedController.selectAction.action.performed += TriggerPressed;
    }

    private void OnDisable()
    {
        actionBasedController.selectAction.action.performed -= TriggerPressed;
    }

    private void TriggerPressed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        slime.SetActive(true);
        Debug.Log("Select button pressed!");
    } */

    
    
    // Start is called before the first frame update
    void Start()
    {
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

        /* // Erstelle einen Ray, der von der Position des Controllers in dessen Vorwärtsrichtung verläuft
        Ray ray = new Ray(actionBasedController.transform.position, actionBasedController.transform.forward);
        // Definiere eine Variable, in der später das getroffene Objekt gespeichert wird
        GameObject hitObject = null;
        // Prüfe, ob der Ray ein Objekt trifft
        if (Physics.Raycast(ray, out RaycastHit hit)) {
            hitObject = hit.collider.gameObject;
        }
        // Prüfe, ob das getroffene Objekt das Tag "Color" hat
        if (hitObject != null && hitObject.CompareTag("Color")) {
            // Hier kannst du Code ausführen, der ausgeführt werden soll, wenn das Objekt mit dem Tag "Color" getroffen wurde
        } */



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

    public void ShootColor()
    {
        slime.SetActive(true);
    }
}
