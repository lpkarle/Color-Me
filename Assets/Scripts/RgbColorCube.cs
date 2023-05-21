using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RgbColorCube : MonoBehaviour
{
    [SerializeField]
    private GameObject ColorIndicator;

    // Update is called once per frame
    void Update()
    {
        Color currentColor = ColorMeGameManager.instance.currentColorShoot;

        //ColorIndicator.GetComponent<Renderer>().material.color = currentColor;

        //ColorIndicator.transform.position += new Vector3(currentColor.r, currentColor.g, currentColor.b); 
    }
}
