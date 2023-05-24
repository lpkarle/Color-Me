using UnityEngine;

public class RgbColorCube : MonoBehaviour
{
    [SerializeField]
    private GameObject ColorIndicator;

    void Update()
    {
        Color currentColor = ColorMeGameManager.Instance.currentColorShoot;
    }
}
